using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "Powers/GoblinPunch")]
public class GoblinPunch : Power {

	public int baseDamage;
	public int damageMult;
    //public new string name = "Goblin Punch";

	public override void usePower(CombatChar caster, CombatChar[] targets){
		int lvl = targets[0].C.lvl;
		float damage = caster.C.attributes[Attributes.Strength].GetMod(lvl);
		damage += caster.C.skills[Skills.Wrestling].GetMod(lvl);
		damage += caster.C.attributes[Attributes.Vitality].GetMod(lvl);
		//damage /= 3;
		damage += caster.C.Properties[Properties.PowerDamage].GetValue();
		damage /= targets[0].C.percentLife()/100;
		damage += Random.Range(baseDamage/2, baseDamage *1.5f);
		targets[0].takeDamage(damage, false);
			//Debug.Log("powwx");

	} 

	
}
