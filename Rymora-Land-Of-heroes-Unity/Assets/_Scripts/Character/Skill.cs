using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill : Raiser {

	public static float diff = 5;
	public static int mod = 5;
	public static int baseVal = 5;

	public Skill(string sk, Character owner) : base(sk,  diff, baseVal, owner) {
		this.modifier = mod;
	}

}
