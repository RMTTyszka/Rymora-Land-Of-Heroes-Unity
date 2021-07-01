using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : Effects {

	public float ticks = 3f;
	private float ticksCount;

	public float castDamage;

	public override void Effect (CombatChar target)
	{	
		targetChar = target;
		targetChar.takeDamage(castDamage, false, false, true);
		target.sprite.GetComponent<SpriteRenderer>().color = Color.green;
		target.C.fillLife.color = Color.green;

	}
	public override void Start ()
	{
		countdown = 0f;

	}
	public override void Update ()
	{
		countdown += Time.deltaTime;
		if (countdown >= ticksCount) {
			ticksCount += ticks;
			Effect(targetChar);

		}
		if (countdown >= maxDuration) {
			RemoveEffect();
		}

	}
	public override void RemoveEffect ()
	{
		targetChar.RemoveEffect(this.GetComponent<Effects>());
		targetChar.sprite.GetComponent<SpriteRenderer>().color = Color.white;
		targetChar.C.fillLife.color = Color.red;
	}




}
