using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller{


	public string name;
	public int value;
	public int totalBonus;
	public Bonuses bonuses;
	public int modifier;

	public Roller(string name, int val) {
		this.name = name != null ? name : "default";
		this.value = val;
		this.bonuses = new Bonuses();
		this.totalBonus = bonuses.getBon();
	}
	public  int Roll() {
		//Debug.Log("roller roll called");
		int sum = 0;
		for (int x = 0; x < GetMod(); x++) {
			sum += Random.Range(0,2);
		} 
		return sum;
	}

	public virtual int GetValue() {
		return value + totalBonus;
	}
	public virtual int GetMod() {
		return GetValue()/modifier;
	}
	//public virtual int GetMod(int newMod) {
		//return GetValue()/newMod;
	//}

	public void addBonus(Bonus bonus) {
		bonuses.addBonus(bonus);
		totalBonus = bonuses.getBon();
	}
	public void removeBonus(Bonus bonus) {
		bonuses.removeBonus(bonus);
		totalBonus = bonuses.getBon();
	}


}
