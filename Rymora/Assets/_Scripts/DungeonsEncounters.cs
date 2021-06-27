using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonsEncounters : MonoBehaviour {

	public Dictionary<string, object> orcFort = new Dictionary<string, object>();

	void Start() 
	{
		orcFort.Add("Encounters",new string[] {"Goblin Warrior", "Goblin Peasant"});
		foreach(string monster in orcFort["Encounters"])
		{
			print (monster);
		}
	}
}
