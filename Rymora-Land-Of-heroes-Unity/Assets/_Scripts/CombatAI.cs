using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAI : MonoBehaviour{

	public enum AITests {Life, Mana, Armor, Effect};
	public enum AIConditions {Lesser, Higher, Equal};
	//public enum AIEffects = Effects.EffectList;
	public enum AIValues {_100 = 100, _90 = 90,  _80 = 80, _70 = 70, _60 = 60, _50 = 50, _40 = 40, _30 = 30, _20 = 20, _10 = 10, _0 = 0,
									Dead, Stunned, Frozen, Poison};

	public List<CombatAIRow> AIRows = new List<CombatAIRow>();

	private CombatChar owner;


	void Awake() {
		owner = GetComponent<CombatChar>();
	}

	public void CheckPower() {
		foreach (CombatAIRow row in AIRows) {
			if (row.test == AITests.Life) {
				if (checkLife(row)) {
					break;
				}
			}
			if (row.test == AITests.Armor) {
				if (checkLife(row)) {
					break;
				}
			}
		}
	}
	


	private bool checkLife(CombatAIRow row) {
		//Debug.Log(owner);
		List<CombatChar> targets = owner.GetTargetsOnRange(row.power.range, row.targets);
		if (targets == null) {
			return false;
		}
		List<CombatChar> realTargets = new List<CombatChar>();



		if (row.condition == AIConditions.Higher) {
			foreach (CombatChar target in targets) {
				if (target.C.percentLife() >= (float)row.value) {
					realTargets.Add(target);
				}
			} 
		}
		else if (row.condition == AIConditions.Lesser) {
			foreach (CombatChar target in targets) {
				//Debug.Log(target.percentLife() + " " + (float)row.value);
				if (target.C.percentLife() <= (float)row.value) {
					realTargets.Add(target);
				}
			}
		}
		if (realTargets.Count == 0) {
			return false;
		} else if (realTargets.Count == 1) {
			owner.castTarget = realTargets[0];
			owner.chargedPower = row.power;
			owner.castingTimer = row.power.castingTime;
			return true;
		} else {
			realTargets.Sort(delegate(CombatChar a, CombatChar b) {
				return (a.GetAggro().CompareTo(b.GetAggro()));
			});
			owner.castTarget = realTargets[realTargets.Count-1];
			owner.chargedPower = row.power;
			owner.castingTimer = row.power.castingTime;
			return true;
		}
	}
}
