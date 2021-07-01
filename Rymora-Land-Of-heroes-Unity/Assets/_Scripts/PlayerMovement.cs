using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed = 10;

	private Vector3 newPos;

	// Use this for initialization
	void Start () {
		newPos = new Vector3(0,0,0);
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		newPos.x = Input.GetAxis("Horizontal")*speed*Time.deltaTime;
		newPos.z = Input.GetAxis("Vertical")*speed*Time.deltaTime;
		transform.position += newPos;
	
	}
}
