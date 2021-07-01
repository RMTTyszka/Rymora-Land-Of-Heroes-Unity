using System.Collections;
using System.Collections.Generic;
//using UnityEngine;

public  enum BonusType  {Equipment, Effect, Consumable, Race, Trait, Furniture};
public class Bonus {

	public string bonus;
	public string type;
	public int value;

	public Bonus(BonusList bonus, BonusType type, int value) {
		this.bonus = bonus.ToString();
		this.type = type.ToString();
		this.value = value;
	}
}
