using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint missleLauncher;
    public TurretBluePrint moneyFactory;
    public TurretBluePrint wallTurret;
    public TurretBluePrint meleeTurret;
    public TurretBluePrint AOEMeleeTurret;

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

    public void SelectMoneyFactory()
    {
        Debug.Log("Money Factory Selected");
        buildManager.SelectTurretToBuild(moneyFactory);
    }

    public void SelectWallTurret()
    {
        Debug.Log("Wall Turret Selected");
        buildManager.SelectTurretToBuild(wallTurret);
    }

    public void SelectMeleeTurret()
    {
        Debug.Log("Melee Turret Selected");
        buildManager.SelectTurretToBuild(meleeTurret);
    }

    public void SelectAOEMeleeTurret()
    {
        Debug.Log("Melee Turret Selected");
        buildManager.SelectTurretToBuild(AOEMeleeTurret);
    }
}
