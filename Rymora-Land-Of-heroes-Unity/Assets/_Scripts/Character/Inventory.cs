using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;




public class Inventory : MonoBehaviour, IDragHandler, IBeginDragHandler {

    public Character Owner;
    public Text ownerText;

    private float offSetX;
    private float offSetY;
    public List<Item> itens;
    public float weight = 0;


    // Use this for initialization
    void Start () {
       // Owner = GetComponentInParent<Character>();
        ownerText.text = Owner.name;
        itens = new List<Item>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void AddItem(Item item) {
        
        itens.Add(item);
        weight += item.weight * item.quantity;
        Owner.weight = weight;
       
    }
    public void RemoveItem(Item item) {
        itens.Remove(item);
        weight -= item.weight*item.quantity;
        Owner.weight = weight;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        return;
        offSetX = transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        offSetY = transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        return;

        transform.position = new Vector3(offSetX + Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                            offSetY + Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f);
        //throw new System.NotImplementedException();
    }



}
