using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Power : ScriptableObject {

	//public  string name;
	public float castingTime;
	public int manaCost;
	public int numberTarget;
	public string description;
	public float range;
    

	public void usePower (CombatChar caster, CombatChar target) {
		CombatChar[] targets = {target};
		usePower(caster,targets);
	}
	public virtual void usePower(CombatChar caster, CombatChar[] targets) {
		
	}



    
}
