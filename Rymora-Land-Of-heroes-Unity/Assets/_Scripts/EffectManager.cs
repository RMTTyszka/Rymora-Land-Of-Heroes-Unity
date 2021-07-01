using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

	private Character C;
	private CombatChar combatChar;

	private List<Effects> effects;


	// Use this for initialization
	void Start () {
		C = transform.root.GetComponent<Character>();
		combatChar = transform.root.GetComponent<CombatChar>();
		effects = new List<Effects>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RemoveEffect(Effects effect) {
		foreach (Effects ef in effects) {
			if (ef == effect) {
				effects.Remove(effect);
				Destroy(effect.gameObject);
				break;
			}
		}
	}
	public bool StackEffect(Effects effect) {
		foreach (Effects ef in effects) {
			if (ef.name == effect.name) {
				ef.maxDuration += effect.maxDuration;
				return true;
			}
		}
		return false;
	}


	public void AddEffect(Effects effect) {
		if (effect.isStackable){
			if (StackEffect(effect)) {
				Destroy(effect.gameObject);
				return;
			} 
		}
		effects.Add(effect);
		effects.Sort(delegate(Effects x, Effects y) {
			return (x.countdown).CompareTo(y.countdown);
		});
		effect.Effect(combatChar);
		
		int count = 0;
		foreach(Effects ef in effects) {
			ef.transform.SetParent(transform);
			//ef.transform.parent = transform;
			RectTransform rect = ef.GetComponent<RectTransform>();
			rect.localScale = new Vector3(1,1,1);
			ef.transform.SetSiblingIndex(count);
			count++;
		}
	}
}
