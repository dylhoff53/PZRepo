using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;

    [Header("Optional")]
    public GameObject tower;

    public Vector3 positionOffset;

    private Renderer rend;
    public Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    /*
    public void OnMouseDown() {

    }

    public void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        MouseEnterCheck();
    }

    private void OnMouseExit()
    {
        ResetColors();
    }
    */
    public void ResetColors()
    {
        rend.material.color = startColor;
        if (BuildManager.selectedAbility != null && BuildManager.selectedAbility.hasSecondaryAttack)
        {
            foreach (Collider node in BuildManager.selectedAbility.nodes)
            {
                if (node.GetComponent<Node>() != this)
                {
                    node.GetComponent<Node>().rend.material.color = node.GetComponent<Node>().startColor;
                }
            }
        }
    }

    public void MouseEnterCheck() {

        if (!buildManager.CanBuild && BuildManager.selectedAbility == null)
            return;

        if (buildManager.CanBuild == true && buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else if (BuildManager.selectedAbility == null)
        {
            rend.material.color = notEnoughMoneyColor;
        }

        if (BuildManager.selectedAbility != null)
        {
            rend.material.color = BuildManager.selectedAbility.abilityColor;
            BuildManager.selectedAbility.targetNode = transform;
            if (BuildManager.selectedAbility.hasSecondaryAttack)
            {
                Vector3 bufferedHalfExtents = new Vector3(BuildManager.selectedAbility.secondaryHalfExtents.x - 0.05f, BuildManager.selectedAbility.secondaryHalfExtents.y, BuildManager.selectedAbility.secondaryHalfExtents.z - 0.05f);
                BuildManager.selectedAbility.nodes = Physics.OverlapBox(transform.position, bufferedHalfExtents, Quaternion.identity, BuildManager.selectedAbility.nodeLayerMask);
                foreach (Collider node in BuildManager.selectedAbility.nodes)
                {
                    if (node.GetComponent<Node>() != this)
                    {
                        node.GetComponent<Node>().rend.material.color = BuildManager.selectedAbility.secondaryColor;
                    }
                }

            }
        }
    }

    public void MouseDownCheck()
    {
        if (!buildManager.CanBuild && BuildManager.selectedAbility == null)
            return;

        if (tower != null && BuildManager.selectedAbility == null)
        {
            Debug.Log("Can't Build there! - TODO: Display on Screen.");
            return;
        }
        else if (BuildManager.selectedAbility == null)
        {
            buildManager.BuildTurretOn(this);
            rend.material.color = startColor;
        }
        else if (BuildManager.selectedAbility != null && BuildManager.selectedAbility.isTargetedAbility)
        {
            BuildManager.selectedAbility.UseAbility();
            ResetColors();
        }
    }
}
