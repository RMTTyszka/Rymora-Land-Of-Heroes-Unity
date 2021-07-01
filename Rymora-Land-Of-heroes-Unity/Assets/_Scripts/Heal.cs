using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Powers/Heal")]
public class Heal : Power {

	public int baseHeal;
	public float healMult;

	public override void usePower (CombatChar caster, CombatChar[] targets)
	{
		int lvl = targets[0].C.lvl;
		float healAmount =  caster.C.attributes[Attributes.Wisdom].Roll(lvl);
		healAmount += caster.C.skills[Skills.Healing].Roll(lvl);
		healAmount += caster.C.skills[Skills.Magery].Roll(lvl);
		healAmount *= healMult;
		healAmount += Random.Range(baseHeal/2f, baseHeal*1.5f);

		targets[0].takeHealing(healAmount);
		//base.usePower (caster, targets);
	}
}
