using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {

	public Sprite[] backgrounds;
	private Dictionary<string, Sprite> bgDict =
		 new Dictionary<string, Sprite>();

	void Start() {
		
		foreach(Sprite bg in backgrounds){
			bgDict.Add(bg.name,bg);
		}
		this.GetComponent<SpriteRenderer>().sprite = bgDict[GlobalData.background];
	}
	void Update() {

	}
}
