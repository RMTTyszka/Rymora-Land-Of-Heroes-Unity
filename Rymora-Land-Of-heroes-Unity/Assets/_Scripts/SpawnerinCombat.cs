﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerinCombat : MonoBehaviour {

	public float radius;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
