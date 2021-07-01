using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : MonoBehaviour {

    public EquipManager eManager;
    public Slots slot;
    public Equipable item;

    public void Equip(Equipable itemToEquip) {
        item = itemToEquip;
        if (slot == Slots.None) {

        }
        eManager.Owner.C.Equip(item, slot); 
    }
	

	// Use this for initialization
	void Start () {
        eManager = GetComponentInParent<EquipManager>();
		
	}
	// Update is called once per frame
	void Update () {
		
	}
}
