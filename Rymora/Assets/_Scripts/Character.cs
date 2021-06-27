using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour {
	

	private Inventory inventory = new Inventory();
	private Dictionary<string, int> attributes = 
		new Dictionary<string, int>();
	private Dictionary<string, int> skills = 
		new Dictionary<string, int>();
	private Dictionary<string, int> defenses = 
		new Dictionary<string, int>();
	private Dictionary<string, int> resists = 
		new Dictionary<string, int>();
	private Dictionary<string, object> equipment = 
		new Dictionary<string, object>();
	private Dictionary<string, int> properties = 
		new Dictionary<string, int>();
	private Dictionary<string, int> bonuses = 
		new Dictionary<string, int>();
	private Dictionary<string, string> effects = 
		new Dictionary<string, string>();
	private Dictionary<string, string> powers = 
		new Dictionary<string, string>();
	private Dictionary<string, int> prop = 
		new Dictionary<string, int>();
	//private Dictionary<string, object> inventory = 
		//new Dictionary<string, object>();
	public Character() 
	{
		foreach (string attr in GlobalData.attributes)
		{
			this.attributes.Add(attr, 0);
			Debug.Log(attr);
		}
		foreach (string sk in GlobalData.skills)
		{
			this.skills.Add(sk, 0);
			Debug.Log(sk);
		}
		foreach (string def in GlobalData.defenses)
		{
			this.defenses.Add(def, 0);
			Debug.Log(def);
		}
		foreach (string res in GlobalData.resists)
		{
			this.resists.Add(res, 0);
			Debug.Log(res);
		}
		foreach (string slot in GlobalData.slots)
		{
			this.equipment.Add(slot, null);
			Debug.Log(slot);
		}
		foreach (string prop in GlobalData.prop)
		{
			this.properties.Add(prop, 0);
			Debug.Log(prop);
		}

	}
	void Start()
	{
		
	}


	void Update()
	{
		
	}
}