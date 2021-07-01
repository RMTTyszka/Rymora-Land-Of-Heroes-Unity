using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatAIRow {

	public CombatManager.Targets targets;
	public CombatAI.AITests test;
	public CombatAI.AIConditions condition;
	public CombatAI.AIValues value;
	public Power power;

    public CombatAIRow(string targets, string tests, string conditions, string values, string power ) {
        
    }

    public CombatAIRow() {

    }

}
