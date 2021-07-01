using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatChar : MonoBehaviour {

	public LayerMask enemy;
	public LayerMask allies;
	public List<CombatChar> enemyTargets;
	public List<CombatChar> alliesTargets;
	public CombatChar castTarget;
	public Power chargedPower;
	public GameObject CBTpref;
	public float castingTimer;
	public Vector3 initialPos;



	public bool isCombat = false;
	public bool isActive = true;
	public bool isMoving = false;
	public bool isTarget = true;
	public bool isCasting = false;
	public bool canBeInterruped = false;
	public bool isAlive = true;

	public CombatAI combatAI;
	public Transform sprite;
	public SpriteRenderer spriteR;
	public SpriteOutline spriteOutline;
	public EffectManager buffManager;
	public List<Weapons> weapons = new List<Weapons>();
	public string weap;

	public Character C; 
	// Use this for initialization
	void Awake () {
		C = GetComponent<Character>();
		combatAI = GetComponent<CombatAI>();
		sprite = GetComponentInChildren<SpriteRenderer>().transform;
		spriteOutline = sprite.GetComponent<SpriteOutline>();
		spriteR = sprite.GetComponent<SpriteRenderer>();
		initialPos = sprite.position - transform.position;
		Weapons wep = Weapons.CreateWeaponBySize(Weapons.WeaponsSize.Light, 1);
		weapons.Add(wep);
		weap = wep.weapon.ToString();
		Armor armor = Armor.GetArmorByCat(Armor.ArmorCat.Light, 1);
		C.equipment[Slots.Chest] = armor;


	}

	// Update is called once per frame
	void Update () {
        // Make one sprite appears before the others that are UP to the screen
		spriteR.sortingOrder =  Mathf.RoundToInt(transform.position.y * 100f) * -1;
        // Run combat checks
		if (isCombat && isAlive) {
			if (isActive) {
				if (!isMoving && !isCasting) {
                    // Recuce the time from the attack countdown.
					foreach (Weapons wep in weapons) {
                        // If the char hasn't attacked for a time, it resets the attack countdown, but just as half of the speed. 
						if (wep.attackCooldown > -wep.attackSpeed/2) {
							wep.attackCooldown -= Time.deltaTime * (1 + C.Properties[Properties.AttackSpeed].GetValue()/100f);						
						} else {
							wep.attackCooldown += wep.attackSpeed;
						}
					}
                    // reduce the time from the casting countdown
					castingTimer -= Time.deltaTime * (1 + C.Properties[Properties.CastingSpeed].GetValue()/100f);

				}
                // Check each weapon to see if its time to attack
				foreach (Weapons wep in weapons) {
					if (wep.attackCooldown <= 0 && !isMoving && isActive && !isCasting) {
                        // Get a list of possibler targets in range of its weapon
						List<CombatChar> targets = GetTargetsOnRange(wep.range, CombatManager.Targets.Enemies);
                        // ignore weapons attack if there is no target in range. 
						if (targets == null ) {
							//Debug.Log("no target");
							continue;
						} else {
                            // Get one target based on its Threat level. 
							CombatChar target = GetAttackTarget(targets);
                            // Check is has LOS and perform the attack
							if (InLOS(target.transform)){
								StartCoroutine(AttackAnim(wep, target, false));
								wep.attackCooldown += wep.attackSpeed;
							}
						}
					}
				}
                // Decide which power is going to be used. 
				if (castTarget == null && chargedPower == null && castingTimer <= 0f) {
					combatAI.CheckPower();
				}
                if (castTarget != null && chargedPower != null && castingTimer <= -0.5f) {
					castTarget.sprite.GetComponent<SpriteOutline>().enabled = false;
                    castTarget = null;
                    chargedPower = null;
					combatAI.CheckPower();
                }
                // Show casting spell name and give the chance to be interrupted. 
				if (castTarget != null && chargedPower != null && castingTimer <= 1.5f && !canBeInterruped) {
					InitCBT(chargedPower.name, "SpellName");
					canBeInterruped = true;
				}
                // Check is can cast the spell and then cast it
				if (castingTimer <= 0 && castTarget != null && chargedPower != null && castTarget.isTarget && !isMoving && castTarget.isAlive) {
					if (checkInRange(castTarget, chargedPower.range) && InLOS(castTarget.transform)) {
						chargedPower.usePower(this, castTarget);
					}
                    // Clean ewverything about the power and the target
					castTarget.sprite.GetComponent<SpriteOutline>().enabled = false;
					castTarget = null;
					chargedPower = null;
					canBeInterruped = false;
				}
			}
		}

	}
	public bool checkInRange(CombatChar target, float range) {
        // Function used just before the attack or cast

        // If the target is itself you are always in range
        if (target != this)
        {
            float distance = target.GetComponent<Collider2D>().Distance(transform.GetComponent<Collider2D>()).distance;
		    if (distance <= range) {
			    return true;
		    } else {
			    return false;
		    }

        }
        else {
            return true;
        }

		//Vector3 closestPoint = castTarget.GetComponent<Collider2D>().ClosestPointOnBounds(transform.position); 
		//float distance = Vector3.Distance(transform.position, closestPoint);
	}

	public void Attack(Weapons weapon, CombatChar target, bool isCounter) {
        // Perform the rolls to see if the attacks was succeded and if its a counter attack already, it doesn't cause another counter.  

		int attkRoll = C.skills[weapon.attackSkill].GetMod(target.C.lvl);
			attkRoll += C.attributes[weapon.attackAttr].GetMod(target.C.lvl);
			attkRoll += C.Properties[Properties.Attack].GetValue();
			attkRoll += (int)weapon.hitMod;
			attkRoll += Random.Range(1, 101);
			//attkRoll += CheckForArmorAndWeaponCritical(target);
			int evadeRoll = target.EvadeRoll(this.C.lvl);
			//Debug.Log("attacking " + attkRoll + " x " + evadeRoll );
			if (attkRoll >= evadeRoll) {
				bool isCrit = CriticalRoll(weapon, target);
				float damage = Damage(target ,weapon, isCrit);
				target.takeDamage(damage, isCrit, isCounter);
			} else if (!isCounter){
				target.CheckForCounter(weapon, this);
			}

	}
	public bool CriticalRoll(Weapons weapon, CombatChar target) {
        //Check if it was a critical hit, comparing the attacker and the target

		int crit = C.Properties[Properties.Critical].GetValue() + 15;
		crit += CheckForArmorAndWeaponCritical(weapon, target);
		int resilience = target.C.Properties[Properties.Resiliense].GetValue();
		int roll = Random.Range(0,100);
		//Debug.Log(roll + " " + crit +" " + resilience);
		if (roll <= crit-resilience) {
		//	Debug.Log("CritouU");
			return true;
		} else {
			return false;
		}
	}
	public float Damage(CombatChar target, Weapons weapon, bool isCrit) {
        // Causa the damage, based on the attacker, the weapon and the target attributes

		float wepDamage = Random.Range(weapon.minDamage, weapon.maxDamage);
		wepDamage = (weapon.minDamage+ weapon.maxDamage)/2;
		//Debug.Log(wepDamage);
		wepDamage += weapon.bonusDamage.getBon();
		wepDamage *= weapon.multDamage;
		// get damage between characters stats
        //ToDo
		float charDamage = damageModifier(weapon.attackSkill, target.C.lvl);
		charDamage -= target.Fortitude(target.C.lvl);
		charDamage *= weapon.multAttr;
		charDamage += C.attributes[weapon.damageAttr].GetMod(target.C.lvl)*2;
		//Debug.Log(charDamage);
		//get protection
		float prot = target.Protection() - ArmorPenetration(weapon);
		prot = prot < 0 ? 0 : prot;
		//Debug.Log("prot Bonus == " + properties["protection"].GetValue());
		//Debug.Log(prot);
		//Debug.Log(ArmorPenetration(wep));
		float total = wepDamage + charDamage - prot;
		total = total < 0 ? 0 : total;
		total *= isCrit ? 2f + C.Properties[Properties.CriticalDamage].GetValue()/100f : 1;
		return total;
	}

	public float damageModifier(Skills skill, int lvl) {
        // Skill + ArmsLore*2 + Bonus
		return (C.skills[skill].GetMod(lvl)
			+ C.skills[Skills.Armslore].GetMod(lvl)*2
				 + C.Properties[Properties.AttackDamage].GetValue());
	}
	public float magicDamageModifier(Skills skill, int lvl) {
        // Skill + Lore*2 + Bonus
		return (C.skills[skill].GetMod(lvl)
			+ C.skills[Skills.Lore].GetMod(lvl)*2
				 + C.Properties[Properties.PowerDamage].GetValue());
	}
	public float Fortitude(int lvl) {
        // Parry + Vit + Bonus
		return (C.skills[Skills.Parry].GetMod(lvl) 
			+ C.attributes[Attributes.Vitality].GetMod(lvl) 
				+ C.Properties[Properties.Fortitude].GetValue());
	}
	public float MagicDefense(int lvl) {
        // Resisti Spells + Int + Bonus
		return (C.skills[Skills.ResistSpells].GetMod(lvl) 
			+ C.attributes[Attributes.Intuition].GetMod(lvl) 
				+ C.Properties[Properties.PowerDefense].GetValue());
	}
	public float Protection() {
        // Armor + Bonus
		Armor armor = C.equipment[Slots.Chest] as Armor;
		
		return (C.Properties[Properties.Protection].GetValue()
			+ armor.protection);
	}
	public float ArmorPenetration(Weapons weapon) {
		return (weapon.armorPen
				+ C.Properties[Properties.ArmorPen].GetValue());
	}
	public void takeDamage(float damage, bool isCrit, bool isCounter = false, bool isMagic = false) {
        if (isAlive) {
		    C.life -= Mathf.RoundToInt(damage);
		    if (isCrit) {
			    InitCBT(Mathf.RoundToInt(damage).ToString(), "CriticalDamage");
		    } else if(isCounter) {
			    InitCBT(Mathf.RoundToInt(damage).ToString(), "Damage", true);
			
		    } else {
			    InitCBT(Mathf.RoundToInt(damage).ToString(), "Damage");
		    }
		    if (C.life < 0) {
			    C.life = 0;
			    isAlive = false;
			    isTarget = false;
			    isActive = false;
			    Debug.Log(C._name + " has died");
			    this.sprite.GetComponent<SpriteRenderer>().color = Color.gray;
                this.spriteOutline.enabled = false;
                Die();
			    //Destroy(gameObject);
            }
		}
	}

	public void takeHealing(float healAmount) {
		//Debug.Log(string.Format("{0} got healed: {1} and had {2} of life", this._name, heal, life)); 

		C.life += Mathf.RoundToInt(healAmount);
		InitCBT(Mathf.RoundToInt(healAmount).ToString(), "Heal");
		if (C.life > C.maxLife()) {
			C.life = C.maxLife();
			isAlive = true;
			isTarget = true;
			//Destroy(gameObject);
		}
	}

	public void CheckForCounter(Weapons weapon, CombatChar target) {
		int counter = (int)weapon.counterRating + C.Properties[Properties.Counter].GetValue();
		counter += CheckForArmorAndWeaponCounter(weapon, target);
		int roll = Random.Range(0,100);
		if (roll <= counter) {
			StartCoroutine(AttackAnim( weapon, target, true));
//			int lvlDif = target.C.lvl;
//			//StartCoroutine(CounterAnim(target));
//			//Debug.Log("Target is: "+attackTarget);
//			if (target.isTarget && target != null) {
//				int attkRoll = C.skills[weapon.attackSkill].GetMod(lvlDif);
//				attkRoll += C.attributes[weapon.attackAttr].GetMod(lvlDif);
//				attkRoll += C.Properties[Properties.Attack].GetValue();
//				attkRoll += (int)weapon.hitMod;
//				attkRoll += Random.Range(1, 101);
//				//attkRoll += CheckForArmorAndWeaponCritical(target);
//				int evadeRoll = target.EvadeRoll(lvlDif);
//				//Debug.Log("attacking " + attkRoll + " x " + evadeRoll );
//				if (attkRoll >= evadeRoll) {
//					bool isCrit = CriticalRoll(weapon,target);
//					float damage = Damage(target ,weapon, isCrit);
//					target.takeDamage(damage, isCrit);
//				} 
//			}
		}
	}
	public int EvadeRoll(int lvl) {
        // Agi + Tactics + Bonus + Armor
		Armor armor = C.equipment[Slots.Chest] as Armor;
		return (C.attributes[Attributes.Agility].GetMod(lvl)
			+ C.skills[Skills.Tactics].GetMod(lvl)
				+ C.Properties[Properties.Evasion].GetValue() 
				+ armor.evasion
			+ Random.Range(1, 101));
	}
	public float ReactionRoll(int lvl) {
        // Used to evade spells 
        // Reflex + Int + BOnus

		return (C.skills[Skills.Reflex].GetMod(lvl) 
			+ C.attributes[Attributes.Intuition].GetMod(lvl) 
			+ C.Properties[Properties.Reaction].GetValue()
			+ Random.Range(1,101));
	}

	public int CheckForArmorAndWeaponCritical(Weapons weapon, CombatChar target) {
		Armor arm = target.C.equipment[Slots.Chest] as Armor;
		if (weapon.size == Weapons.WeaponsSize.Light && arm.category == Armor.ArmorCat.Light) {
			return 30;
		}
		else if (weapon.size == Weapons.WeaponsSize.Heavy && arm.category == Armor.ArmorCat.Heavy) {
			return 30;
		}
		else if (weapon.size == Weapons.WeaponsSize.Medium) {
			return 10;
		} else {
			return -15;
		}
	}

	public int CheckForArmorAndWeaponCounter(Weapons weapon, CombatChar target) {
		Armor arm = target.C.equipment[Slots.Chest] as Armor;
		if (weapon.size == Weapons.WeaponsSize.Light && arm.category == Armor.ArmorCat.Heavy) {
			return 30;
		}
		else if (weapon.size == Weapons.WeaponsSize.Heavy && arm.category == Armor.ArmorCat.Light) {
			return 30;
		}
		else {
			return 0;
		}
	}

	public CombatChar GetAttackTarget(List<CombatChar> targets) {
        // Get target from a list of possible targets, ahd pick the one with the highest aggro

		if (targets == null) {
			return null;
		}
		CombatChar target = null;
		foreach (CombatChar tar in targets) {
			if (target == null) {
				target = tar;
			} else {
				if (tar.GetAggro() > target.GetAggro()) {
					target = tar;
				}
			}
		}
		return target;
	}

	public bool InLOS(Transform target) {
        // Check if the target is in LOS right before the attack,
        // It Uses the box collider, not the sprite. 

        // If the target is yourself you are always in LOS
		if (target == this.transform) {
			return true;
		} 

		BoxCollider2D coll = target.GetComponent<BoxCollider2D>();
		float range = Vector2.Distance(target.position, transform.position);
		Vector2 targetPos =  (Vector2)target.position - coll.offset;
		range = Vector2.Distance(transform.position,targetPos);
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, targetPos-(Vector2)transform.position, range);
		foreach (RaycastHit2D hit in hits) {
			Debug.DrawRay(transform.position, targetPos - (Vector2)transform.position, Color.green);
			//Debug.DrawLine(transform.position, hit.transform.position, Color.cyan);
			if (hit.transform == transform) {
                // ignore self
				continue;
			} else if (hit.transform.CompareTag("lowWall")){
                // Used to give penality and ignore it for now
				continue;
			} else if (!hit.transform.CompareTag(target.tag)){
				return false;
			}
		}
		return true;
	}

	public List<CombatChar> GetTargetsOnRange(float range,CombatManager.Targets t) {
		// Get a list of possible targets, based if they are friendly or foe

		List<CombatChar> targets = new List<CombatChar>();
		LayerMask mask = t == CombatManager.Targets.Enemies ? enemy : allies;
		Collider2D[] hitCollider = Physics2D.OverlapCircleAll(transform.position, range, mask  );
		foreach (Collider2D coll in hitCollider) {
			CombatChar Char= coll.GetComponent<CombatChar>();
			if (Char.isAlive && Char.isTarget) {
				if (InLOS(Char.transform)) {
					targets.Add(Char);
				}
			}
		}
		if (targets.Count <= 0) {
			return null;
		} else {
			return targets;
		}
	}

	public float GetAggro() {
        // Less health == More Aggro, and bonus
		return ((1f-C.percentLife())*10 + C.Properties[Properties.Threat].GetValue());
	}

	public void InitCBT(string text, string trigger, bool isCounter = false) {
		GameObject CBT = Instantiate(CBTpref);
		RectTransform CBTRect = CBT.GetComponent<RectTransform>();
		CBT.transform.SetParent(transform.Find("CharCanvas"));
		CBTRect.transform.localPosition = CBTpref.transform.localPosition;
		//CBTRect.transform.localScale = CBTpref.transform.localScale;
		CBTRect.transform.localRotation = CBTpref.transform.localRotation;
	 	CBT.GetComponent<Text>().text = text;

	 	CBT.GetComponent<Animator>().SetTrigger(trigger);
	 	if (isCounter) {
			CBT.GetComponent<Text>().color = new Color(0.2f, 0.3f, 0.4f);
	 	}
	 	Destroy(CBT, 2f);
	}
	#region anims
	public IEnumerator AttackAnim( Weapons weapon, CombatChar target, bool isCounter) {

		//Vector3 initialPos = sprite.transform.position;
		Vector3 newPos;
		float count = 0f;
		while(count <= 1f) {
			Vector3 tarPos = target.C.sprite.transform.position;
			count += Time.deltaTime*4;
			newPos = Vector3.Lerp(transform.position, tarPos, count);
			C.sprite.transform.position = newPos;
			if (Vector3.Distance(C.sprite.transform.position, tarPos) <= 1) {
				//inRange = true;
				Attack(weapon, target, isCounter);
				count = 0;
				Vector3 newInitialPos = C.sprite.transform.position;
				while(count <= 1f) {
					count += Time.deltaTime*4;
					newPos = Vector3.Lerp(newInitialPos, transform.position + initialPos, count);
					C.sprite.transform.position = newPos;
					yield return null;
				}
				isMoving = false;
				break;
			}
			yield return null;
		}
	}

	#endregion
	public void AddEffect(Effects effect) {
		buffManager.AddEffect(effect);
	}
	public void RemoveEffect(Effects effect) {
		buffManager.RemoveEffect(effect);
	}
	public void OnMouseDown() {
	}

    public void Die() {
        //Destroy(this.gameObject);
    }


}
