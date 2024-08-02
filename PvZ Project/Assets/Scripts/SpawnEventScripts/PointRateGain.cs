using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRateGain : SpawnableEvent
{
    public float newRate;


    public override void TriggerEvent()
    {
        WaveSpawner.instance.pointGainInterval = newRate;
        Debug.Log("New Point Rate: " +  newRate);
    }
}
