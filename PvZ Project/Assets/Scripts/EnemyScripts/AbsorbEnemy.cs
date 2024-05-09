using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbEnemy : Enemy
{
    public LayerMask detectLayerMask;
    public Vector3 cubeHalfExtents;
    public Collider[] colliders;
    public bool willAbsorb = true;

    public override void FixedUpdate()
    {
        if (isAttacking)
        {
            colliders = Physics.OverlapBox(transform.position, cubeHalfExtents, Quaternion.identity, detectLayerMask);
            if(colliders.Length > 0)
            {
                return;
            }
        }
        else
        {
            March();
        }
    }

    public override void Attack()
    {
        colliders = Physics.OverlapBox(transform.position, cubeHalfExtents, Quaternion.identity, detectLayerMask);
        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                collider.GetComponent<Turret>().BeenHit(damage);
            }
        }
    }

    public void BulletCheck(int bulletDamage)
    {
        if (willAbsorb)
        {
            damage += (bulletDamage / 4);
        }
        else
        {
            Hit(bulletDamage);
        }
        willAbsorb = !willAbsorb;
    }

}
