using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CharMove : MonoBehaviour {

	public Transform attackRange;
    public Pathfinder pathfinder;
    public CharMove testChar;
	//public Transform castingRange;
	public Transform sprite;
	public SpriteOutline outLine;
	public Transform partyNumber;
	private Text partyNumberText;
	public Character Char;
	public CombatChar CombChar;
    public Canvas Menu;
    public AiMenu aiMenu;
	public bool isSelected = false;
    public CharMove isFollowing = null;

    public List<Vector2> wayPoints;

	// Use this for initialization
	void Start () {
		Char = GetComponent<Character>();
		CombChar = GetComponent<CombatChar>();
		outLine = sprite.GetComponent<SpriteOutline>();
		partyNumberText = partyNumber.GetComponent<Text>();
		PartyManager.instance.Parties[0].Add(this);
        //Menu = GetComponentInChildren<Canvas>();
        //aiMenu = GetComponentInChildren<AiMenu>();
        wayPoints = new List<Vector2>();
        if (transform.name == "Ramiro")
        {
          // StartCoroutine (pathfinder.GetWaypoints(transform.position, testChar.transform.position));
        
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (isSelected) {
            if (wayPoints.Count == 0) {
                wayPoints.Add(transform.position);
            }
            float distance = Vector2.Distance(transform.position, wayPoints[wayPoints.Count - 1]);
            if (distance > 0.5f)
            {
                wayPoints.Add(transform.position);
            }
            if (wayPoints.Count > 4) {
               // Debug.Log(wayPoints.Count);
                wayPoints.RemoveAt(0);
            }
        }


		partyNumberText.text = (PartyManager.instance.GetPartyIndex(this)+1).ToString();
		if (!CombChar.isAlive) {
			isSelected = false;
			outLine.enabled = false;
		}
		 ManageTarget();
        if (isFollowing != null) {
            ManageMove(isFollowing);
        }
		if (isSelected && Input.GetKey(KeyCode.A)) {
            Floor floor = pathfinder.ground.GetTile(Vector3Int.FloorToInt(transform.position)) as Floor;
            //Debug.Log(floor.moveMultiplier.ToString());
			transform.position += Vector3.left * Char.speed * Time.deltaTime * floor.moveMultiplier;
		}
		if (isSelected && Input.GetKey(KeyCode.D)) {
            Floor floor = pathfinder.ground.GetTile(Vector3Int.FloorToInt(transform.position)) as Floor;
			transform.position += Vector3.right * Char.speed * Time.deltaTime * floor.moveMultiplier;
		}
		if (isSelected && Input.GetKey(KeyCode.W)) {
            Floor floor = pathfinder.ground.GetTile(Vector3Int.FloorToInt(transform.position)) as Floor;
			transform.position += Vector3.up * Char.speed * Time.deltaTime * floor.moveMultiplier;
		}
		if (isSelected && Input.GetKey(KeyCode.S)) {
            Floor floor = pathfinder.ground.GetTile(Vector3Int.FloorToInt(transform.position)) as Floor;
			transform.position += Vector3.down * Char.speed * Time.deltaTime * floor.moveMultiplier;
		}

		if (CombChar.isCombat && isSelected) {
			updateAttackRange(CombChar.weapons[0].range);
			if (CombChar.chargedPower != null) {
				updateCastingRange(CombChar.chargedPower.range); 
			} else {
				updateCastingRange(); 
				
			}
		} else {
			resetRanges();
		}
        
		#region TabKey
		if (Input.GetKeyDown(KeyCode.Tab)) {
			CombChar.isCombat = !CombChar.isCombat;
			if (CombChar.isCombat) {
				CombatOn();
			} else {
				CombatOff();
			}
		}
		#endregion	
	}

	void OnMouseDown() {
		PartyManager.instance.SelectPartyMember(PartyManager.instance.Parties[PartyManager.instance.currentParty].IndexOf(this));
	}
	
	public void CombatOn() {
		foreach (Weapons wep in CombChar.weapons) {
					wep.attackCooldown = wep.attackSpeed;
			}
		CombChar.castingTimer = 0f;
	}
	public void CombatOff() {
		updateAttackRange();
		//updateCastingRange();
		if (CombChar.castTarget != null) {
			CombChar.castTarget.spriteOutline.enabled = false;
		}
		CombChar.castTarget = null;
		CombChar.chargedPower = null;
	}

	public void updateAttackRange() {
		attackRange.gameObject.SetActive(false);
	}
	public void updateAttackRange(float range) {
		attackRange.gameObject.SetActive(true);
		attackRange.transform.localScale = new Vector3(range, range, 1);
	}
	public void updateCastingRange(float range) {
		//castingRange.gameObject.SetActive(true);
		//castingRange.transform.localScale = new Vector3(range, range, 1);
	}
	public void updateCastingRange() {
		//castingRange.gameObject.SetActive(false);
	}

	public void resetRanges() {
		updateAttackRange();
		//updateCastingRange();
	}
    public void ManageMove(CharMove following) {
        int index = int.Parse(partyNumberText.text);
        index = index - int.Parse(following.partyNumberText.text);
        index = index > 0 ? index : index * -1;
        Floor floor = pathfinder.ground.GetTile(Vector3Int.FloorToInt(transform.position)) as Floor;
        if (following.wayPoints.Count > index) {
            transform.position = Vector2.MoveTowards(
                                                    transform.position, 
                                                    following.wayPoints[index], 
                                                     Char.speed * Time.deltaTime * floor.moveMultiplier
                                                     );
        }
    }



    public void ManageTarget() {
		if (isSelected) {
			outLine.enabled = true;
			outLine.color = Color.white;
			if (CombChar.castTarget && CombChar.isCombat) {
				if (CombChar.castTarget.tag == "Player" && CombChar.checkInRange(CombChar.castTarget, CombChar.chargedPower.range)) {
					CombChar.castTarget.spriteOutline.enabled = true;
					CombChar.castTarget.spriteOutline.color = Color.green;
				} else if (CombChar.castTarget.tag == "Player"){
					CombChar.castTarget.spriteOutline.enabled = true;
					CombChar.castTarget.spriteOutline.color = Color.red;
				} else {
					if (CombChar.checkInRange(CombChar.castTarget, CombChar.chargedPower.range)){
						CombChar.castTarget.spriteOutline.enabled = true;
						CombChar.castTarget.spriteOutline.color = Color.blue;
					} else {
						CombChar.castTarget.spriteOutline.enabled = true;
						CombChar.castTarget.spriteOutline.color = Color.red;
					}
				}
			}
		}
	}

    


}
