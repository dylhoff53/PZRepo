using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public bool isTalent;
    public DraggableItem inSlot;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (transform.childCount == 0 && isTalent == dropped.GetComponent<DraggableItem>().isTalent)
        {
            if(dropped.GetComponent<DraggableItem>().currentInvenSlot != null)
            {
                dropped.GetComponent<DraggableItem>().currentInvenSlot.inSlot = null;
            }
            MoveIn(dropped.GetComponent<DraggableItem>());
        } else if(transform.childCount == 1 && isTalent == dropped.GetComponent<DraggableItem>().isTalent && dropped.GetComponent<DraggableItem>().Storeage.isPlaced == false)
        {
            inSlot.Storeage.Reset();
            MoveIn(dropped.GetComponent<DraggableItem>());
        } else if (transform.childCount == 1 && isTalent == dropped.GetComponent<DraggableItem>().isTalent && dropped.GetComponent<DraggableItem>().Storeage.isPlaced == true)
        {
            Debug.Log("TEST!");
            dropped.GetComponent<DraggableItem>().currentInvenSlot.MoveIn(inSlot);
            inSlot.transform.SetParent(dropped.GetComponent<DraggableItem>().currentInvenSlot.transform);
            MoveIn(dropped.GetComponent<DraggableItem>());
        }
    }

    public void MoveIn(DraggableItem DI)
    {
        inSlot = DI;
        inSlot.parentAfterDrag = transform;
        inSlot.Storeage.isPlaced = true;
        inSlot.currentInvenSlot = this;
    }
}
