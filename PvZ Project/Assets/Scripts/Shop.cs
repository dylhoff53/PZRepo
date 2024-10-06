using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBluePrint[] turretBlueprints;
    public SelectedLoadout cL;
    public float uiYOffset;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
        for(int i = 0; i < cL.Loadouts.Length; i++)
        {
            if(cL.Loadouts[i] != null)
            {
                GameObject ui = Instantiate(cL.Loadouts[i]);
                ui.transform.SetParent(transform, false);
                turretBlueprints[i] = ui.GetComponent<TurretUIButtonConnect>().bp;
            }
        }

    }

    private void Update()
    {
        foreach (TurretBluePrint blueprint in turretBlueprints)
        {
            if (blueprint.prefab != null && !blueprint.OffCooldown)
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

    public void Selected(TurretBluePrint blue, float uiIndict)
    {
        for(int i = 0; i < turretBlueprints.Length; i++)
        {
            if(blue == turretBlueprints[i])
            {

                SelectTower(turretBlueprints[i], uiIndict);
            }
        }
    }

    public void SelectTower(TurretBluePrint bp, float uiIndict)
    {
        if (bp.spawnCount == 0 && bp.isTalent)
        {
            IndicatorOn(uiIndict);
            buildManager.SelectTurretToBuild(bp);
            BuildManager.selectedAbility = null;
        }
        else if (bp.isTalent)
        {
            BuildManager.instance.indicator.SetActive(false);
            bp.talentUI.UpgradeCheck(bp);
            BuildManager.selectedAbility = null;
        }
        else if(!bp.isTalent)
        {
            IndicatorOn(uiIndict);
            buildManager.SelectTurretToBuild(bp);
            BuildManager.selectedAbility = null;
        }
    }

    public void IndicatorOn(float uiIndict)
    {
        BuildManager.instance.indicator.SetActive(true);
        BuildManager.instance.indicator.GetComponent<RectTransform>().position = new UnityEngine.Vector3(uiIndict, uiYOffset, 0f);
        BuildManager.instance.indicator.GetComponent<RectTransform>().rotation = UnityEngine.Quaternion.Euler(0f, 0f, 180f);
    }
}
