using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TurretBluePrint
{
    public GameObject prefab;
    public int cost;
    public float towerCooldown;
    public float lasttimeBuilt;
    public Slider cooldownSlider;
    public bool OffCooldown = true;
}
