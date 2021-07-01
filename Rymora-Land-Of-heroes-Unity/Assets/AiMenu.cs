using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMenu : MonoBehaviour {

    public CombatAI OwnerAI;
    public AiRowUI aiRowPref;

    public List<AiRowUI> aiRows;
    // Use this for initialization
    public void Awake()
    {
        OwnerAI = GetComponentInParent<CombatAI>();
        aiRows = new List<AiRowUI>();
    }

    void Start () {
        AddRow();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    
    public void AddRow() {

       aiRows.Add(Instantiate(aiRowPref, transform));
       aiRows[aiRows.Count - 1].transform.SetSiblingIndex(transform.childCount - 2);
    }

    public void SaveRows() {
        List<CombatAIRow> rowsList = new List<CombatAIRow>();
        foreach (Transform child in transform) {
            AiRowUI row = child.GetComponent<AiRowUI>();
            if (row != null) {
                rowsList.Add(row.SaveRow());

            }
        }

        OwnerAI.AIRows = rowsList;
    }
}
