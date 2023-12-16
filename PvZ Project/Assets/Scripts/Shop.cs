using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint missleLauncher;
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStanderdTurret()
    {
        Debug.Log("Main Turret Selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectOtherTurret()
    {
        Debug.Log("Other Turret Selected");
        buildManager.SelectTurretToBuild(missleLauncher);
    }
}
