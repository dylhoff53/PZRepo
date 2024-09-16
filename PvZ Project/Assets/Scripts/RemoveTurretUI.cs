using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTurretUI : TalentAbilityScript
{
    public Node node;
    public override void UseAbility()
    {
        Debug.Log("Used Ability!");
        RemoveTurret();
    }

    public void RemoveTurret() 
    {
         node = targetNode.GetComponent<Node>();
        Debug.Log("Here's the node, " + node);
        if (node.tower != null)
        {
            node.tower.GetComponentInChildren<Turret>().Die();
            node.tower = null;
            base.UseAbility();
        } else
        {
            Debug.Log("Error! There's no Tower to Remove!");
        }
    }
}
