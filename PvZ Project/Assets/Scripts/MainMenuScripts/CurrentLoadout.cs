using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLoadout : MonoBehaviour
{
    public InventorySlot[] inventory;

    public GameObject[] selectedLoadout;
    public static CurrentLoadout instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in Scene!");
            gameObject.GetComponent<AudioManager>().masterVolume = instance.gameObject.GetComponent<AudioManager>().masterVolume;
            Destroy(instance.gameObject);
            instance = this;
        }
        else
        {
            instance = this;
        }
    }
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

    public void Clear()
    {
        selectedLoadout = new GameObject[9];
    }
}
