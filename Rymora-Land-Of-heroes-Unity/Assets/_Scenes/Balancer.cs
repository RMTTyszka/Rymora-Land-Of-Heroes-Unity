using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancer : MonoBehaviour {

	public Character charPref;

	public Dictionary<Weapons.WeaponsSize, Dictionary<int, float>> weapons = new Dictionary<Weapons.WeaponsSize, Dictionary<int, float>>();
	public Dictionary<Armor.ArmorCat, Dictionary<int, float>> armors = new Dictionary<Armor.ArmorCat, Dictionary<int, float>>();

	public Dictionary<Weapons.WeaponsSize, Dictionary<int, Character>> charWep = new Dictionary<Weapons.WeaponsSize, Dictionary<int, Character>>();
	public Dictionary<Armor.ArmorCat, Dictionary<int, Character>> charArm = new Dictionary<Armor.ArmorCat, Dictionary<int, Character>>();

	// Use this for initialization
	void Start () {
		Debug.Log("Starto3");
		foreach ( Weapons.WeaponsSize wep in System.Enum.GetValues(typeof(Weapons.WeaponsSize))) {
			weapons.Add(wep, new Dictionary<int, float>());
			charWep.Add(wep, new Dictionary<int, Character>());
		}foreach ( Armor.ArmorCat arm in System.Enum.GetValues(typeof(Armor.ArmorCat))) {
			armors.Add(arm, new Dictionary<int, float>());
			charArm.Add(arm, new Dictionary<int, Character>());
		}


		for ( int x = 0; x < 20; x++) {
			foreach (KeyValuePair<Weapons.WeaponsSize, Dictionary<int, float>> wep in weapons) {
				wep.Value.Add(x+1, 0f);
				charWep[wep.Key].Add(x+1, Instantiate(charPref) as Character);
				charWep[wep.Key][x+1].SetLevel(x+1);
				charWep[wep.Key][x+1].equipment[Slots.Mainhand] = Weapons.CreateWeaponBySize(wep.Key, x+1);
				if (x == 19 && (charWep[wep.Key][x+1].equipment[Slots.Mainhand] as Weapons).size == Weapons.WeaponsSize.Heavy ) {
					Debug.Log((charWep[wep.Key][x+1].equipment[Slots.Mainhand] as Weapons).armorPen);
				}

				}
			foreach (KeyValuePair<Armor.ArmorCat, Dictionary<int, float>> arm in armors) {
				arm.Value.Add(x+1, 0f);
				charArm[arm.Key].Add(x+1, Instantiate(charPref) as Character);
				charArm[arm.Key][x+1].SetLevel(x+1);
				charArm[arm.Key][x+1].equipment[Slots.Chest] = Armor.GetArmorByCat(arm.Key, x+1);
				}


		}

		foreach (KeyValuePair<Weapons.WeaponsSize, Dictionary<int, Character>> wep in charWep) {
			foreach (KeyValuePair<int, Character> lvlw in wep.Value) {
				foreach (KeyValuePair<Armor.ArmorCat, Dictionary<int, Character>> arm in charArm) {
					float sum = 0;
					float damage = 0f;
					float speed = 10000/(lvlw.Value.equipment[Slots.Mainhand] as Weapons).attackSpeed;
					for (int x = 0; x < speed ; x++) {
						damage = lvlw.Value.AttackTest(arm.Value[lvlw.Key]);
						sum += damage;
					}
					sum /= 10000;
					if (wep.Key == Weapons.WeaponsSize.Light && arm.Key == Armor.ArmorCat.Light) {
						while (damage == 0f) {
							damage = lvlw.Value.AttackTest(arm.Value[lvlw.Key]);
						}
						//Debug.Log(lvlw.Key +" Light x Light: " + damage);
					}
					weapons[wep.Key][lvlw.Key] = sum;
					armors[arm.Key][lvlw.Key] = sum;
				}
			}
		}
		foreach(KeyValuePair<Weapons.WeaponsSize, Dictionary<int, float>> wep in weapons) {
			float sum = 0;
			foreach (KeyValuePair<int, float> lvl in wep.Value) {
				sum += lvl.Value;
			}

			Debug.Log("W "+wep.Key.ToString() + " " + sum);
		}
		foreach(KeyValuePair<Armor.ArmorCat, Dictionary<int, float>> arm in armors) {
			float sum = 0;
			foreach (KeyValuePair<int, float> lvl in arm.Value) {
				sum += lvl.Value;
			}

			Debug.Log("A "+arm.Key.ToString() + " " + sum);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}



}
