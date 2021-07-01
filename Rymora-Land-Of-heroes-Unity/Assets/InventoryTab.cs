using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;




public class InventoryTab : MonoBehaviour, IDragHandler, IBeginDragHandler
{

     
    private float offSetX;
    private float offSetY;
    public void OnBeginDrag(PointerEventData eventData)
    {

        offSetX = transform.parent.transform.position.x - Input.mousePosition.x;
        offSetY = transform.parent.transform.position.y - Input.mousePosition.y;
    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.parent.transform.position = new Vector3(offSetX + Input.mousePosition.x,
                                            offSetY + Input.mousePosition.y, 0f);
        //throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}