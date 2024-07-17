using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableEvent
{
    public float spawnTime;
    public bool isEvent;
    public events typeOfEvent;

    public enum events
    {
        pointRateGain,
        pointGain
    }

    public virtual void TriggerEvent()
    {
        Debug.Log("Event Triggered!");
    }

}
