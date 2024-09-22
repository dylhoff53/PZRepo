using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public static GameObject turretParent;
    public string placementSound;
    public float placementSoundVolume;
    public static TalentAbilityScript selectedAbility;
    public Transform activeAbilityParent;
    public GameObject indicator;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager in Scene!");
            return;
        }
        instance = this;
        turretParent = GameObject.Find("Turrets");
    }

    public GameObject buildEffect;


    public static TurretBluePrint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node)
    {
        if(PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        if(!turretToBuild.OffCooldown)
        {
            Debug.Log("Still on Cooldown");
            return;
        }

        if (turretToBuild.prefab.transform.GetChild(0).GetComponent<Turret>().isTalent && turretToBuild.talentUI.talentLevel == 0)
        {
            turretToBuild.talentUI.UpgradeCheck(turretToBuild);
            turretToBuild.talentUI.upgradeImage.SetActive(true);
        } else if(turretToBuild.prefab.transform.GetChild(0).GetComponent<Turret>().isTalent && turretToBuild.talentUI.talentLevel >= 0)
        {
            turretToBuild.talentUI.Respawn();
        }

        turretToBuild.spawnCount++;
        turretToBuild.OffCooldown = false;
        turretToBuild.cooldownSlider.value = 1f;
        turretToBuild.lasttimeBuilt = Time.time;
        indicator.SetActive(false);
        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), node.transform.rotation);
        node.tower = turret;
        if (turretToBuild.isTalent)
        {
            turret.transform.GetChild(0).GetComponent<Talent>().talentUI = turretToBuild.talentUI;
            turretToBuild.talentUI.talent = turret.transform.GetChild(0).GetComponent<Talent>();
            turret.transform.GetChild(0).GetComponent<Talent>().activeAbilityUIparent = activeAbilityParent;
        }
        turret.transform.GetChild(0).gameObject.GetComponent<Turret>().node = node;
        turret.transform.parent = turretParent.transform;
        turret.transform.GetChild(0).gameObject.GetComponent<Turret>().blueprint = turretToBuild;

        turretToBuild = null;
        GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
        AudioManager.PlayOneShot(placementSound, placementSoundVolume, AudioManager.location);

        Debug.Log("Turret build! Money left: " + PlayerStats.Money);

    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
    }
}
