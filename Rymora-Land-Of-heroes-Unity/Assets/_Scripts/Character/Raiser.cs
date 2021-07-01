using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raiser : Roller {

	
	public float difficult;
	private int count = 0;
	private Character owner;


	public Raiser(string name, float diff, int baseVal, Character _owner) : base(name, baseVal) {
		this.difficult = diff;
		this.owner = _owner;
	}
	public  int Roll(int lvl) {
		//Debug.Log("Raiser roll called");
		RollForRaise(lvl);
		return base.Roll();

	}

	public int GetMod (int lvl)
	{
		//Debug.Log("caguei");
		RollForRaise(lvl);
		return GetMod();


	}

	private void RollForRaise(int lvl) {
		if ( lvl >= this.value/5) {
			float chance = 1000f/((value/5f)*difficult);
			float roll = Random.Range(0f, 10001f);
			//Debug.Log(roll + " " + chance);
			if (roll <= chance) {
				//value++;
				owner. UpdateStats();
				Debug.Log(this.name + " aumentou, agora é " + value);
				//chance = (1000f/((value/5f)*difficult))/10000f*100;
				//Debug.Log(chance);
			}
		}
	}
}
