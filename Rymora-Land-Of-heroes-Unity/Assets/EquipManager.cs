using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour {

    public CombatChar Owner;

	// Use this for initialization
	void Start () {
        Owner = transform.root.GetComponent<CombatChar>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
