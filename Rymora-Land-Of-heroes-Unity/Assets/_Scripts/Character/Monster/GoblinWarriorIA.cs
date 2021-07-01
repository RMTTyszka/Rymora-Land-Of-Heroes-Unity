using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinWarriorIA : MonoBehaviour {

	private void AI(){
		Debug.Log("eu tenho logica");
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) this.AI();
		
	}
}
