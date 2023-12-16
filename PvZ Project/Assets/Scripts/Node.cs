using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    private GameObject tower;
    public Vector3 positionOffset;



    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public void OnMouseDown()
    {
        if (buildManager.GetTurrtToBuild() == null)
            return;

        if(tower != null)
        {
            Debug.Log("Can't Build there! - TODO: Display on Screen.");
            return;
        }

        GameObject turrretToBuild = buildManager.GetTurrtToBuild();
        tower = (GameObject)Instantiate(turrretToBuild, transform.position + positionOffset, transform.rotation);
    }

    public void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurrtToBuild() == null)
            return;

        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
