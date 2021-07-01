using System.Collections;
using System.Collections.Generic;
using System.Reflection;
//using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class Character : MonoBehaviour {

	public bool isPlayer;
	public float speed = 3;
	public  string _name = "Lekthris";
	public  string _race = "Gnome";

	public Slider lifeBar;
	public Image fillLife;
	public Slider spiritBar;
	public Transform sprite;
	private CombatChar cB;



	public int lvl;
	public int life;
	public int spiritPoints;
	public int valiantPoints;

    //public bool isAlive = true;
    public float weight = 0f;
	public Inventory inventory;

	public Dictionary<Attributes, Attribute> attributes = 
		new Dictionary<Attributes, Attribute>();
	public Dictionary<Skills, Skill> skills = 
		new Dictionary<Skills, Skill>();
	public Dictionary<string, Defense> defenses = 
		new Dictionary<string, Defense>();
	public Dictionary<string, Resist> resists = 
		new Dictionary<string, Resist>();
	public Dictionary<Slots, Equipable> equipment = 
		new Dictionary<Slots, Equipable>();
	public Dictionary<Properties, Property> Properties = 
		new Dictionary<Properties, Property>();
	public Dictionary<string, Property> properties = 
		new Dictionary<string, Property>();
	public Dictionary<string, string> effects = 
		new Dictionary<string, string>();
	public Dictionary<string, Power> knownPowers = 
		new Dictionary<string, Power>();

	



	public void EventManager(MyEvent eve) {
		
	}

	public void AddBonus(Bonus bonus) {
		if (GlobalData.attributes.Contains<string>(bonus.bonus.ToString())) {
			//attributes[bonus.bonus].bonuses.addBonus(bonus);
		} else if (GlobalData.skills.Contains<string>(bonus.bonus)) {
			//skills[bonus.bonus].bonuses.addBonus(bonus);
		} else if (GlobalData.Resists.Contains<string>(bonus.bonus)) {
			resists[bonus.bonus].bonuses.addBonus(bonus);
		} else if (GlobalData.Defenses.Contains<string>(bonus.bonus)) {
			defenses[bonus.bonus].bonuses.addBonus(bonus);
		} else if (GlobalData.Properties.Contains<string>(bonus.bonus)) {
			properties[bonus.bonus].bonuses.addBonus(bonus);
		} else {
			Debug.Log("The name of the bonus is not known by the character class");
		}

	}
	public int maxLife () {
		int baseLife = 500;
		int vitLife = this.attributes[Attributes.Vitality].GetValue() * 10;

		return 	baseLife + vitLife;
	}
	public float percentLife() {
		return ((float)life)/((float)maxLife())*100;
	}

	public int maxSP() {
		int baseSP = 50;
		int intSP = this.attributes[Attributes.Intuition].GetMod() * 10;
		return baseSP + intSP;
	}
	public int percentSP() {
		return (spiritPoints*100)/(maxSP()*100)/100;
	}


	public float AttackTest( Character target) {
		Weapons weapon = equipment[Slots.Mainhand] as Weapons;
		int lvlDif = target.lvl;
		//Debug.Log("Target is: "+attackTarget);
		if (target.cB.isTarget && target != null) {
			int attkRoll = skills[weapon.attackSkill].GetMod(lvlDif);
			attkRoll += attributes[weapon.attackAttr].GetMod(lvlDif);
			attkRoll += properties["attack"].GetValue();
			attkRoll += (int)weapon.hitMod;
			attkRoll += Random.Range(1, 101);
			//attkRoll += CheckForArmorAndWeaponCritical(target);
			int evadeRoll = target.cB.EvadeRoll(lvlDif);
			//Debug.Log("attacking " + attkRoll + " x " + evadeRoll );
			if (attkRoll >= evadeRoll) {
				float damage = cB.Damage(target.GetComponent<CombatChar>() ,weapon, cB.CriticalRoll(weapon, target.GetComponent<CombatChar>()));
				return damage;
			} else {
				return 0f;
			}
		} else {
			Debug.Log("No target Available");
		}
		return 0f;
	}



		





	public void Awake() {
		cB = GetComponent<CombatChar>();

		foreach (Attributes attr in System.Enum.GetValues(typeof(Attributes)))
		{
			Attribute attribute = new Attribute(attr.ToString(), this);
			this.attributes.Add(attr, attribute);
			//Debug.Log(attr+ " " +this.getFlatValue(attr));
 		}
		foreach (Skills sk in System.Enum.GetValues(typeof(Skills)))
		{
			Skill skill = new Skill(sk.ToString(), this);
			this.skills.Add(sk, skill);
			//Debug.Log(sk);
		}
		foreach (string def in GlobalData.Defenses)
		{
			Defense defense = new Defense(def);
			this.defenses.Add(defense.name, defense);
			//Debug.Log(def);
		}
		foreach (string res in GlobalData.Resists)
		{
			Resist resist = new Resist(res);
			this.resists.Add(resist.name, resist);
			//Debug.Log(res);
		}
		foreach (Slots slot in System.Enum.GetValues(typeof(Slots)))
		{
			this.equipment.Add(slot, null);
			//Debug.Log(slot);
		}
		foreach (string prop in GlobalData.Properties)
		{
			Property property = new Property(prop);
			this.properties.Add(property.name, property);
			//Debug.Log(prop);
		}
		foreach (Properties prop in System.Enum.GetValues(typeof(Properties)))
		{
			Property property = new Property(prop.ToString());
			this.Properties.Add(prop, property);
			//Debug.Log(attr+ " " +this.getFlatValue(attr));
		}


		//inventory = new Inventory(this);
		life = maxLife();
        Power pow =Resources.Load<GoblinPunch>("Goblin Punch") as Power;
        knownPowers.Add(pow.name, pow);
        pow =Resources.Load<Heal>("Heal") as Power;
        knownPowers.Add(pow.name, pow);
        pow =Resources.Load<Poison>("Poison") as Power;
        knownPowers.Add(pow.name, pow);
	}

	void Start() {
	}

	void Update() {

		lifeBar.value = percentLife()/100;
	}



	public void UpdateStats() {
		lvl = 0;
		List<int> list = new List<int>();
		foreach(KeyValuePair<Attributes, Attribute> attr in attributes) {
			list.Add(attr.Value.value/5);
		}
		list.Sort();
		for (int x = 0; x < 3; x++) {
			lvl += list[x];
		}
		foreach(KeyValuePair<Skills, Skill> sk in skills) {
			list.Add(sk.Value.value/5);
		}
		list.Sort();
		for (int x = 0; x < 5; x++) {
			lvl += list[x];
		}

		lvl /= 8;

	}

	public void SetLevel(int level) {
		foreach (KeyValuePair<Attributes, Attribute> attr in attributes) {
			attr.Value.value = 5*level;
		}
		foreach (KeyValuePair<Skills, Skill> sk in skills) {
			sk.Value.value = 5*level;
		}
	}

	public void Equip( Equipable item, Slots slot) {
        Equipable oldItem = null;
        if (item.slot == slot || item.slot == Slots.Mainhand && slot == Slots.Offhand && (item as Weapons).size != Weapons.WeaponsSize.Heavy) {
            if (equipment[slot] != null) {
                 oldItem = equipment[slot];
            }
            equipment[slot] = item;
            foreach (Bonus bonus in item.bonuses) {

            }

        }


//		if (equip.slot == Slots.Mainhand) {
//			Weapons wep = equip as Weapons;
//			if (wep.size == Weapons.WeaponsSize.Heavy) {
//				weapons.Clear();
//				weapons.Add(wep);
//				equipment[Slots.Mainhand] = wep as Equipable;
//				equipment[Slots.Offhand] = null;
//			} else {
//				weapons.Add(wep);
//				equipment[Slots.Mainhand] = wep as Equipable;
//				if (weapons.Count >= 3) {
//					weapons.RemoveAt(0);
//
//				}
//				if (weapons.Count >= 2) {
//					equipment[Slots.Offhand] = wep as Equipable;
//					foreach (Weapons weapon in weapons) {
//						weapon.hitMod += -25;
//					}
//
//				}
//			}
//		}
	}
	public void Unequip (Equipable equip) {
		
	}

}