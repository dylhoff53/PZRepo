using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    private GameObject tower;
    public Vector3 positionOffset;



    private Renderer rend;
    private Color startColor;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public void OnMouseDown()
    {
        if(tower != null)
        {
            Debug.Log("Can't Build there! - TODO: Display on Screen.");
            return;
        }

        GameObject turrretToBuild = BuildManager.instance.GetTurrtToBuild();
        tower = (GameObject)Instantiate(turrretToBuild, transform.position + positionOffset, transform.rotation);
    }

    public void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
