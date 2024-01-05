using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDetector : MonoBehaviour
{
    public Turret turret;
    public float radius;
    public LayerMask detectLayerMask;
    public Collider[] colliders;
    public Vector3 cubeHalfExtents;
    public bool attack;

    private void FixedUpdate()
    {
        if(turret.isMelee == true && turret.isAOE == false)
        {
            colliders = Physics.OverlapSphere(transform.position, radius, detectLayerMask);
            if (colliders.Length > 0)
            {
                turret.targetInRange = true;
            }
            else
            {
                turret.targetInRange = false;
            }
        } else if (turret.isMelee == true && turret.isAOE == true)
        {
            colliders = Physics.OverlapBox(transform.position, cubeHalfExtents, Quaternion.identity, detectLayerMask);
            if (colliders.Length > 0)
            {
                turret.targetInRange = true;
                if(attack)
                {
                    foreach (Collider collider in colliders)
                    {
                        collider.GetComponent<Enemy>().Hit(turret.damage);
                    }
                    attack = false;
                }
            }
            else
            {
                turret.targetInRange = false;
            }
        }
    }
}
