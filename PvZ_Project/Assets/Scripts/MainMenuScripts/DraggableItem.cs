using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public bool isTalent;
    [HideInInspector]
    public Transform parentAfterDrag;
    public StorageSlot Storeage;
    public InventorySlot currentInvenSlot;
    public GameObject itemShopUI;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        Storeage.background.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging Drag");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(parentAfterDrag);
        if(transform.parent == Storeage.background.transform.parent)
        {
            Storeage.background.SetActive(false);
        }
        image.raycastTarget = true;
    }

}
