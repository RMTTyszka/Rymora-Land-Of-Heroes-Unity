using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class Effects : MonoBehaviour {

	public enum EffectList {Dead, Stunned, Poisoned, Frozen, Scared};

	public bool isHarmful;
	public bool isStackable;
	public float maxDuration;
	public float countdown;
	public CombatChar targetChar = null;
	public Weapons targetWeap = null;

	public virtual void Start() {
	}
	// Use this for initialization
	public virtual void Effect() {
		
	}
	public virtual void Effect(CombatChar target) {

	}

	// Update is called once per frame
	public virtual void Update () {
		
	}

	public virtual  void RemoveEffect() {

	}
}
