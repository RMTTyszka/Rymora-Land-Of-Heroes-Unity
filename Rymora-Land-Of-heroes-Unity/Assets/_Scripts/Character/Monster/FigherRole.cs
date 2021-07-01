using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigherRole : Role {
	
	void Start(){
		bonuses.Add("strength", 12);
		bonuses.Add("agility", 12);
		bonuses.Add("vitality", 12);
		bonuses.Add("swordmanship", 12);
		bonuses.Add("heavyweaponship", 12);
		bonuses.Add("tactics", 12);
		foreach(KeyValuePair<string, int> bon in bonuses){
			//Debug.Log(bon.Key);
		}
	}
			
	
}
