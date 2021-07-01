using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AiRowUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Dropdown targetTab;
    public Dropdown testTab;
    public Dropdown conditionTab;
    public Dropdown valueTab;
    public Dropdown powerTab;
    public CombatChar Owner;
    public int index;
	// Use this for initialization
	void Start () {
        Owner = GetComponentInParent<CombatChar>();
        // Dropdown.OptionData op = new Dropdown.OptionData();
        // op.text = "coco";
        // targetTab.options.Add(op);
        foreach (string target in Enum.GetNames(typeof(CombatManager.Targets))) {
            Dropdown.OptionData op = new Dropdown.OptionData();
            op.text = target;
            op.image = null;
            targetTab.options.Add(op);
        }
        targetTab.RefreshShownValue();
        foreach (string test in Enum.GetNames(typeof(CombatAI.AITests))) {
            Dropdown.OptionData op = new Dropdown.OptionData();
            op.text = test;
            op.image = null;
            testTab.options.Add(op);
        }
        testTab.RefreshShownValue();
        foreach (string condition in Enum.GetNames(typeof(CombatAI.AIConditions))) {
            Dropdown.OptionData op = new Dropdown.OptionData();
            op.text = condition;
            op.image = null;
            conditionTab.options.Add(op);
        }
        foreach (string value in Enum.GetNames(typeof(CombatAI.AIValues))) {
            Dropdown.OptionData op = new Dropdown.OptionData();
            op.text = value;
            op.image = null;
            valueTab.options.Add(op);
        }
        conditionTab.RefreshShownValue();
        foreach (string  power in Owner.C.knownPowers.Keys) {
            Dropdown.OptionData op = new Dropdown.OptionData();
            op.text = power;
            op.image = null;
            powerTab.options.Add(op);
        }
        powerTab.RefreshShownValue();
      

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpButton()
    {
        if (transform.GetSiblingIndex() > 1)
        {
            transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);
        }
    }
    public void DownButton()
    {
        if (transform.GetSiblingIndex() < transform.parent.childCount-2)
        {
           transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
        }
    }

    public CombatAIRow SaveRow() {
        CombatAIRow row = new CombatAIRow();
        
        foreach (CombatAI.AIConditions condition in Enum.GetValues(typeof(CombatAI.AIConditions))) {
            if (condition.ToString() == conditionTab.GetComponentInChildren<Text>().text ) {
                row.condition = condition;
            }
        }
        foreach (CombatManager.Targets target in Enum.GetValues(typeof(CombatManager.Targets))) {
            if (target.ToString() == targetTab.GetComponentInChildren<Text>().text ) {
                row.targets = target;
            }
        }
        foreach (CombatAI.AIValues values in Enum.GetValues(typeof(CombatAI.AIValues))) {
            if (values.ToString() == valueTab.GetComponentInChildren<Text>().text ) {
                row.value = values;
            }
        }
        foreach (CombatAI.AITests tests in Enum.GetValues(typeof(CombatAI.AITests))) {
            if (tests.ToString() == testTab.GetComponentInChildren<Text>().text ) {
                row.test = tests;
            }
        }
        row.power = Owner.C.knownPowers[powerTab.GetComponentInChildren<Text>().text];

        return row;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
