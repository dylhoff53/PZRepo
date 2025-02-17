using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLoadout : MonoBehaviour
{
    public InventorySlot[] inventory;

    public SelectedLoadout currentLoadout;
    public void LockItIn()
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i].inSlot != null)
            {
                currentLoadout.Loadouts[i] = inventory[i].inSlot.itemShopUI;
            }
        }
    }

    public void Clear()
    {
        currentLoadout.Loadouts = new GameObject[9];
    }
}
