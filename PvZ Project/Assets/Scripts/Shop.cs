using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint missleLauncher;
    public TurretBluePrint moneyFactory;
    public TurretBluePrint wallTurret;
    public TurretBluePrint meleeTurret;
    public TurretBluePrint AOEMeleeTurret;

    public TurretBluePrint[] turretBlueprints;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    private void Update()
    {
        foreach (TurretBluePrint blueprint in turretBlueprints)
        {
            float timeCheck = Time.time - blueprint.lasttimeBuilt;
            if (timeCheck <= blueprint.towerCooldown && !blueprint.OffCooldown)
            {
                float percent = Time.deltaTime / blueprint.towerCooldown;
                blueprint.cooldownSlider.value -= percent;
            } else
            {
                blueprint.OffCooldown = true;
                blueprint.cooldownSlider.value = 0f;
            }
        }
    }


    public void SelectStanderdTurret()
    {
        Debug.Log("Main Turret Selected");
        buildManager.SelectTurretToBuild(turretBlueprints[0]);
    }

    public void SelectOtherTurret()
    {
        Debug.Log("Other Turret Selected");
        buildManager.SelectTurretToBuild(turretBlueprints[1]);
    }

    public void SelectMoneyFactory()
    {
        Debug.Log("Money Factory Selected");
        buildManager.SelectTurretToBuild(turretBlueprints[2]);
    }

    public void SelectWallTurret()
    {
        Debug.Log("Wall Turret Selected");
        buildManager.SelectTurretToBuild(turretBlueprints[3]);
    }

    public void SelectMeleeTurret()
    {
        Debug.Log("Melee Turret Selected");
        buildManager.SelectTurretToBuild(turretBlueprints[4]);
    }

    public void SelectAOEMeleeTurret()
    {
        Debug.Log("Melee Turret Selected");
        buildManager.SelectTurretToBuild(turretBlueprints[5]);
    }

    public void SelectInaTurret()
    {
        Debug.Log("Melee Turret Selected");
        if(turretBlueprints[6].spawnCount == 0 && turretBlueprints[6].isTalent)
        {
            buildManager.SelectTurretToBuild(turretBlueprints[6]);
        } else if(turretBlueprints[6].isTalent)
        {
            turretBlueprints[6].talentUI.UpgradeCheck(turretBlueprints[6]);
        }
    }
}
