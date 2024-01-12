using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager in Scene!");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject otherTurretPrefab;
    public GameObject moneyFactoryPrefab;

    public GameObject buildEffect;


    private TurretBluePrint turretToBuild;

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
        turretToBuild.OffCooldown = false;
        turretToBuild.cooldownSlider.value = 1f;
        turretToBuild.lasttimeBuilt = Time.time;

        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), node.transform.rotation);
        node.tower = turret;

        turret.transform.GetChild(0).gameObject.GetComponent<Turret>().node = node;

        GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        Debug.Log("Turret build! Money left: " + PlayerStats.Money);

    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
    }
}
