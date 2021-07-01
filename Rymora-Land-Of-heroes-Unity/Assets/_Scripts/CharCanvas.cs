using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharCanvas : MonoBehaviour {
	public Transform attackBar;
	public Transform castingBar;
	private  Slider attackSlider;
	private  Slider castingSlider;
	private CombatChar combatChar;
	// Use this for initialization
	void Start () {
		combatChar = transform.root.GetComponent<CombatChar>();
		attackSlider = attackBar.GetComponent<Slider>();
		castingSlider = castingBar.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		if (combatChar.isCombat) {
			attackBar.gameObject.SetActive(true); 
			castingBar.gameObject.SetActive(true); 
			attackSlider.maxValue = combatChar.weapons[0].attackSpeed;
			attackSlider.value = attackSlider.maxValue - combatChar.weapons[0].attackCooldown;

			if (combatChar.chargedPower != null) {
				castingSlider.maxValue = combatChar.chargedPower.castingTime;
				castingSlider.value = castingSlider.maxValue - combatChar.castingTimer;
			} else {
				castingSlider.value = 0;
				
			}
		} else {
			attackBar.gameObject.SetActive(false);
			castingBar.gameObject.SetActive(false); 
			
		}
	}
}
