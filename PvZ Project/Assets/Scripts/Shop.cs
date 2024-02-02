using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBluePrint[] turretBlueprints;
    public CurrentLoadout cL;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
        cL = FindObjectOfType<CurrentLoadout>();
        for(int i = 0; i < cL.selectedLoadout.Length; i++)
        {
            if(cL.selectedLoadout[i] != null)
            {
                GameObject ui = Instantiate(cL.selectedLoadout[i]);
                ui.transform.SetParent(transform, false);
            }
        }

    }

    private void Update()
    {
        foreach (TurretBluePrint blueprint in turretBlueprints)
        {
            if (!blueprint.OffCooldown)
            {
                if(!blueprint.isTalent || blueprint.isTalent && blueprint.spawnCount == 0)
                {
                    float percent = Time.deltaTime / blueprint.towerCooldown;
                    blueprint.cooldownSlider.value -= percent;
                }
                
                if (blueprint.cooldownSlider.value <= 0f)
                {
                    blueprint.OffCooldown = true;
                    blueprint.cooldownSlider.value = 0f;
                }
            }
        }
    }


    public void SelectStanderdTurret()
    {
        Debug.Log("Surcorn Selected");
        buildManager.SelectTurretToBuild(turretBlueprints[0]);
        BuildManager.selectedAbility = null;
    }

    public void SelectOtherTurret()
    {
        Debug.Log("Other Turret Selected");
        buildManager.SelectTurretToBuild(turretBlueprints[1]);
        BuildManager.selectedAbility = null;
    }

    public void SelectMoneyFactory()
    {
        Debug.Log("KFP Selected");
        buildManager.SelectTurretToBuild(turretBlueprints[2]);
        BuildManager.selectedAbility = null;
    }

    public void SelectWallTurret()
    {
        Debug.Log("Tako Selected");
        buildManager.SelectTurretToBuild(turretBlueprints[3]);
        BuildManager.selectedAbility = null;
    }

    public void SelectMeleeTurret()
    {
        Debug.Log("Deadbeat Selected");
        buildManager.SelectTurretToBuild(turretBlueprints[4]);
        BuildManager.selectedAbility = null;
    }

    public void SelectAOEMeleeTurret()
    {
        Debug.Log("Chumbud Selected");
        buildManager.SelectTurretToBuild(turretBlueprints[5]);
        BuildManager.selectedAbility = null;
    }

    public void SelectInaTurret()
    {
        Debug.Log("Ina Selected");
        if(turretBlueprints[6].spawnCount == 0 && turretBlueprints[6].isTalent)
        {
            buildManager.SelectTurretToBuild(turretBlueprints[6]);
            BuildManager.selectedAbility = null;
        } else if(turretBlueprints[6].isTalent)
        {
            turretBlueprints[6].talentUI.UpgradeCheck(turretBlueprints[6]);
            BuildManager.selectedAbility = null;
        }
    }

    public void SelectTalent2Turret()
    {
        Debug.Log("Ame Selected");
        if (turretBlueprints[7].spawnCount == 0 && turretBlueprints[7].isTalent)
        {
            buildManager.SelectTurretToBuild(turretBlueprints[7]);
            BuildManager.selectedAbility = null;
        }
        else if (turretBlueprints[7].isTalent)
        {
            turretBlueprints[7].talentUI.UpgradeCheck(turretBlueprints[7]);
            BuildManager.selectedAbility = null;
        }
    }
}
