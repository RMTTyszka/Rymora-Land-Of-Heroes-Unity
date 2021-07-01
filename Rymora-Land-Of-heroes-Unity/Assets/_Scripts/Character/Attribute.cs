using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute: Raiser {

	public static float diff = 10f;
	public static int mod = 5;
	public static int baseVal = 5;


	public Attribute(string attr, Character owner) : base(attr,  diff, baseVal, owner) {
		this.modifier = mod;
	}

	
}
