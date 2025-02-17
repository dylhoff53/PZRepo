using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmeTalentAbility : TalentAbilityScript
{
    public int damage;
    public int secondaryDamage;
    public Vector3 poundHalfExtents;
    public Collider[] hitEnemies;
    public Vector3 nodeOffset;
    public LayerMask layerMask;

    public override void UseAbility()
    {
        Vector3 origin = new Vector3(targetNode.position.x + nodeOffset.x, targetNode.position.y + nodeOffset.y, targetNode.position.z + nodeOffset.z);
        hitEnemies = Physics.OverlapBox(origin, poundHalfExtents, Quaternion.identity, layerMask);
        if(hitEnemies.Length > 0)
        {
            Debug.Log("HIT!");
            foreach (Collider collider in hitEnemies)
            {
                collider.GetComponent<Enemy>().Hit(damage);
            }
        }

        if (hasSecondaryAttack)
        {
            Vector3 secondAttack = new Vector3(targetNode.position.x + nodeOffset.x, targetNode.position.y + nodeOffset.y, targetNode.position.z + nodeOffset.z);
            hitEnemies = Physics.OverlapBox(secondAttack, secondaryHalfExtents, Quaternion.identity, layerMask);
            if(hitEnemies.Length > 0)
            {
                Debug.Log("Second Hit!");
                foreach (Collider collider in hitEnemies)
                {
                    collider.GetComponent<Enemy>().Hit(secondaryDamage);
                }
            }
        }
        base.UseAbility();
    }

}
