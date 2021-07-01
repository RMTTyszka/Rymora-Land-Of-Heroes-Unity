using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public new string name;
    public bool isStackable = false;
    public int quantity = 1;

    public Text quantityText;
    public Transform itemGroundCanvas;
    public Canvas itemFloatingCanvas;
    public LayerMask mask;
    private Image image;
    public Character owner;
    private bool isDraggable = false;
    private Transform parent;
    public float weight = 1f;

    public bool isSelected = false;

    private float offSetX = 0f;
    private float offSetY = 0f;


	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        quantityText.text = quantity.ToString();
        parent = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
        if (isDraggable)
        {
            if (isSelected)
            {
                Vector3 newPos = new Vector3(offSetX + Input.mousePosition.x,
                                                offSetY + Input.mousePosition.y, 0f);


                transform.parent.position = newPos;
            }
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        //Character possibleOwner = PartyManager.instance.currentMember.GetComponent<Character>();
        //if (possibleOwner == null)
        //{
        //    isDraggable = false;
        //    return;
        //}
        //// Check if the owner is the same that is moving the item
        //if (owner != null && owner == possibleOwner)
        //{
        //    Debug.Log("same owner");
        //    isDraggable = true;
        //    offSetX = transform.parent.position.x - Input.mousePosition.x;
        //    offSetY = transform.parent.position.y - Input.mousePosition.y;

        //}
        //// Check if the possible owner is another char and if he is in range
        //else if (owner != null && owner != possibleOwner && Vector2.Distance(possibleOwner.transform.position, owner.transform.position) < 3f)
        //{
        //    Debug.Log("Other owner");
        //    owner.inventory.RemoveItem(this);
        //    owner = possibleOwner;
        //    owner.inventory.AddItem(this);

        //    transform.parent.SetParent(itemFloatingCanvas.transform, true);
        //    offSetX = transform.parent.position.x - Input.mousePosition.x;
        //    offSetY = transform.parent.position.y - Input.mousePosition.y;
        //    isDraggable = true;
        //}
        //else if (owner != null && owner != possibleOwner && Vector2.Distance(possibleOwner.transform.position, owner.transform.position) > 3f)
        //{
        //    Debug.Log("Other owner but too far away");
        //    isDraggable = false;
        //    return;
        //}
        ////Check if the possible owner is in range;
        //else if (owner == null && Vector2.Distance(possibleOwner.transform.position, this.transform.position) > 3f)
        //{
        //    Debug.Log("new owner, off range");
        //    isDraggable = false;
        //    owner = null;
        //    return;
        //}
        //// if possible owner is in range
        //else
        //{
        //    Debug.Log("new owner, in Range range");
        //    isDraggable = true;
        //    owner = possibleOwner;
        //    owner.inventory.AddItem(this);
        //    transform.parent.SetParent(itemFloatingCanvas.transform, true);

        //    offSetX = transform.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        //    offSetY = transform.transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        //    transform.parent.localScale = new Vector3(50, 50, 50);
        //}

        ////throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if (isDraggable) {
        //    Vector3 newPos = new Vector3(offSetX + Input.mousePosition.x,
        //                                    offSetY + Input.mousePosition.y, 0f);

 
        //    transform.parent.position = newPos;
        //    //throw new System.NotImplementedException();
        //}
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if (isDraggable) {
        //    //Code to be place in a MonoBehaviour with a GraphicRaycaster component
        //    GraphicRaycaster gr = GetComponent<GraphicRaycaster>();
        //    //Create the PointerEventData with null for the EventSystem
        //    PointerEventData ped = new PointerEventData(EventSystem.current);
        //    //Set required parameters, in this case, mouse position
        //    ped.position = Input.mousePosition;
        //    //Create list to receive all results
        //    List<RaycastResult> results = new List<RaycastResult>();
        //    //Raycast it
        //    EventSystem.current.RaycastAll(ped, results);
        //    foreach (RaycastResult uiElement in results)
        //    {
        //         //Debug.Log(uiElement.gameObject.name);

        //        Inventory invi = uiElement.gameObject.GetComponent<Inventory>();
        //        Item item = uiElement.gameObject.GetComponent<Item>();
        //        if (item == this) {
        //            continue;
        //        }
        //        if (item != null) {
        //            if (item.isStackable)
        //            {
        //                item.Stack(this);
        //                return;
        //            }
        //            else {
        //                transform.parent.position = new Vector2(item.transform.position.x - 0.2f,
        //                                                    item.transform.position.y + 0.2f);
        //            }
        //        }
        //        if (invi != null)
        //        {
        //            //this.parent.transform.SetParent(invi.transform);

        //            // if has a owner and its putting in the inventory of another character
        //            if (owner != null && owner.inventory != invi)
        //            {
        //                owner.inventory.RemoveItem(this);
        //                invi.AddItem(this);
        //                owner = invi.Owner;
        //                transform.parent.SetParent(owner.inventory.transform,true);
                        
        //                return;
        //            }
        //            // if is the inventory of the same owner
        //            else if (owner != null && owner.inventory == invi) {
        //                transform.parent.SetParent(owner.inventory.transform,true);
        //                return;
        //            }
        //            // if its a new owner with no previous one
        //            else {
        //                transform.parent.SetParent(owner.inventory.transform,true);
        //                return;
        //               // owner = invi.Owner;
        //                //invi.AddItem(this);
        //            }

        //        }

        //    }
        //    //if the item is put on the ground
        //    transform.parent.SetParent(itemGroundCanvas.transform);
        //    transform.parent.localScale = new Vector3(1, 1, 1);
        //    Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    newPos.z = 0;
        //    transform.parent.position = newPos;
        //    if (owner != null) {
        //        owner.inventory.RemoveItem(this);
        //        owner = null;
        //    }
        //    //throw new System.NotImplementedException();
        //}
    }

    public void Stack(Item newIt) {
        if (newIt.name == name && newIt.isStackable && isStackable) {
            quantity += newIt.quantity;
            quantityText.text = quantity.ToString();
            if (owner != null) {
                owner.inventory.weight += newIt.weight * newIt.quantity;
            }
            Destroy(newIt.transform.parent.gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelected == false)
        {
            Character possibleOwner = PartyManager.instance.currentMember.GetComponent<Character>();
            if (possibleOwner == null)
            {
                isDraggable = false;
                isSelected = false;
                return;
            }
            // Check if the owner is the same that is moving the item
            if (owner != null && owner == possibleOwner)
            {
                Debug.Log("same owner");
                isDraggable = true;
                isSelected = true;
                offSetX = transform.parent.position.x - Input.mousePosition.x;
                offSetY = transform.parent.position.y - Input.mousePosition.y;

            }
            // Check if the possible owner is another char and if he is in range
            else if (owner != null && owner != possibleOwner && Vector2.Distance(possibleOwner.transform.position, owner.transform.position) < 3f)
            {
                Debug.Log("Other owner");
                owner.inventory.RemoveItem(this);
                owner = possibleOwner;
                owner.inventory.AddItem(this);

                transform.parent.SetParent(itemFloatingCanvas.transform, true);
                offSetX = transform.parent.position.x - Input.mousePosition.x;
                offSetY = transform.parent.position.y - Input.mousePosition.y;
                isDraggable = true;
                isSelected = true;
            }
            else if (owner != null && owner != possibleOwner && Vector2.Distance(possibleOwner.transform.position, owner.transform.position) > 3f)
            {
                Debug.Log("Other owner but too far away");
                isDraggable = false;
                isSelected = false;
                return;
            }
            //Check if the possible owner is in range;
            else if (owner == null && Vector2.Distance(possibleOwner.transform.position, this.transform.position) > 3f)
            {
                Debug.Log("new owner, off range");
                isDraggable = false;
                isSelected = false;
                owner = null;
                return;
            }
            // if possible owner is in range
            else
            {
                Debug.Log("new owner, in Range range");
                isDraggable = true;
                isSelected = true;
                owner = possibleOwner;
                owner.inventory.AddItem(this);
                transform.parent.SetParent(itemFloatingCanvas.transform, true);

                offSetX = transform.transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                offSetY = transform.transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
                transform.parent.localScale = new Vector3(50, 50, 50);
            }
        }
        else {
            if (isDraggable)
            {
                //Code to be place in a MonoBehaviour with a GraphicRaycaster component
                GraphicRaycaster gr = GetComponent<GraphicRaycaster>();
                //Create the PointerEventData with null for the EventSystem
                PointerEventData ped = new PointerEventData(EventSystem.current);
                //Set required parameters, in this case, mouse position
                ped.position = Input.mousePosition;
                //Create list to receive all results
                List<RaycastResult> results = new List<RaycastResult>();
                //Raycast it
                EventSystem.current.RaycastAll(ped, results);
                foreach (RaycastResult uiElement in results)
                {
                    //Debug.Log(uiElement.gameObject.name);

                    Inventory invi = uiElement.gameObject.GetComponent<Inventory>();
                    Item item = uiElement.gameObject.GetComponent<Item>();
                    if (item == this)
                    {
                        continue;
                    }
                    if (item != null)
                    {
                        if (item.isStackable)
                        {
                            item.Stack(this);
                            return;
                        }
                        else
                        {
                            transform.parent.position = new Vector2(item.transform.position.x - 0.2f,
                                                                item.transform.position.y + 0.2f);
                        }
                    }
                    if (invi != null)
                    {
                        //this.parent.transform.SetParent(invi.transform);

                        // if has a owner and its putting in the inventory of another character
                        if (owner != null && owner.inventory != invi)
                        {
                            owner.inventory.RemoveItem(this);
                            invi.AddItem(this);
                            owner = invi.Owner;
                            transform.parent.SetParent(owner.inventory.transform, true);
                            isSelected = false;
                            return;
                        }
                        // if is the inventory of the same owner
                        else if (owner != null && owner.inventory == invi)
                        {
                            transform.parent.SetParent(owner.inventory.transform, true);
                            isSelected = false;
                            return;
                        }
                        // if its a new owner with no previous one
                        else
                        {
                            transform.parent.SetParent(owner.inventory.transform, true);
                            return;
                            // owner = invi.Owner;
                            //invi.AddItem(this);
                        }

                    }

                }
                //if the item is put on the ground
                isSelected = false;
                transform.parent.SetParent(itemGroundCanvas.transform);
                transform.parent.localScale = new Vector3(1, 1, 1);
                Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newPos.z = 0;
                transform.parent.position = newPos;
                if (owner != null)
                {
                    owner.inventory.RemoveItem(this);
                    owner = null;
                }
                //throw new System.NotImplementedException();
            }
        }
    }
}
