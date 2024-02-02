using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLoadout : MonoBehaviour
{
    public InventorySlot[] inventory;

    public GameObject[] selectedLoadout;

    public void LockItIn()
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i].inSlot != null)
            {
                selectedLoadout[i] = inventory[i].inSlot.itemShopUI;
            }
        }
    }
}
