using System.Collections;
using System.Collections.Generic;

public class Bonuses {

	public Dictionary<string,List<Bonus>> bonuses = new  Dictionary<string,List<Bonus>>();


	public Bonuses () {
		foreach (string bonType in System.Enum.GetNames(typeof(BonusType))) {
			bonuses.Add(bonType, new List<Bonus>());
		}
	}

	public int getBon() {
		int equip = 0;
		int effect = 0;
		int consuma = 0;
		int race = 0;
		int trait = 0;
		int furniture = 0;


		foreach (Bonus bon in bonuses["Equipment"]) {
			equip += bon.value;
		}
		foreach (Bonus bon in bonuses["Effect"]) {
			if (bon.value > effect) {
				effect = bon.value;
			}
		}
		foreach (Bonus bon in bonuses["Consumable"]) {
			if (bon.value > consuma) {
				consuma = bon.value;
			}
		}
		foreach (Bonus bon in bonuses["Race"]) {
			if (bon.value > race) {
				race = bon.value;
			}
		}
		foreach (Bonus bon in bonuses["Trait"]) {
			if (bon.value > trait) {
				trait = bon.value;
			}
		}
		foreach (Bonus bon in bonuses["Furniture"]) {
			if (bon.value > furniture) {
				furniture = bon.value;
			}
		}

		return equip + effect + consuma + race + trait + furniture;
	}

	public void addBonus(Bonus bonus) {
		bonuses[bonus.type].Add(bonus);
	}
	public void removeBonus(Bonus bon) {
		bonuses[bon.type].Remove(bon);
	}

}
