using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Powers/Poison")]
public class Poison : Power {

	public Transform poisonEffect;

	public override void usePower (CombatChar caster, CombatChar[] targets)
	{
		for (int count = 0; count < 1; count++) {
			PoisonEffect effect = Instantiate(poisonEffect).GetComponent<PoisonEffect>();
			effect.castDamage = 10;
			targets[count].AddEffect(effect as Effects);
		}
	}
	
}
