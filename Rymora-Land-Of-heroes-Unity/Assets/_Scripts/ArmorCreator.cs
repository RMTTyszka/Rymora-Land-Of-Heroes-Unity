using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  ArmorCreator : MonoBehaviour {

	public static ArmorCreator instance = null;

	void Awake() {
		if 	(instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
	}
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static Armor createArmor(Armor.ArmorBases armorBase,int lvl) {
		Armor newArmor = new Armor();
		newArmor.getArmorStats(armorBase, lvl);
		return newArmor;
	}
}
