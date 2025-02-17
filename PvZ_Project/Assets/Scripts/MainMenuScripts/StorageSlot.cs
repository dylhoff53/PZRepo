using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageSlot : MonoBehaviour
{
    public DraggableItem DI;
    public bool isPlaced;
    public GameObject background;

    public void Reset()
    {
        if (isPlaced)
        {
            background.SetActive(false);
            DI.transform.SetParent(transform);
            isPlaced = false;
        }
    }
}
