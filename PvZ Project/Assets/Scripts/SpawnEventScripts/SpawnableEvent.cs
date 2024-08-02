using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[System.Serializable]
public class SpawnableEvent
{
    public float spawnTime;
    public bool isEvent;
    public events typeOfEvent;

    public float newPointRateGain;

    public int numberOfPointsToGain;

    public enum events
    {
        pointRateGain,
        pointGain
    }

    public virtual void TriggerEvent()
    {
        Debug.Log("Event Triggered!");
    }

    public void DoEvent()
    {
        if (typeOfEvent == events.pointRateGain) {
            WaveSpawner.instance.pointGainInterval = newPointRateGain;
            Debug.Log("AI point rate is now: " + newPointRateGain);
        } else if (typeOfEvent == events.pointGain)
        {
            WaveSpawner.instance.points += numberOfPointsToGain;
            Debug.Log("AI Gained " + numberOfPointsToGain + " points!");
        }
    }
}
