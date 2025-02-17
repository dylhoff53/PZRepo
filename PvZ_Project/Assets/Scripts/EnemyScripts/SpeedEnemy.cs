using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEnemy : Enemy
{
    public float baseSpeed;

    public override void Update()
    {
        if (isAttacking)
        {
            attackCooldown += Time.deltaTime;
            if (attackCooldown >= attackRate || bonusHealth > 0)
            {
                Attack();
                attackCooldown = 0f;
            }
        }
    }
    public override void BonusHealthBreak()
    {
        parent.GetComponent<EnemyMovement>().speed = baseSpeed;
        base.BonusHealthBreak();

    }

    public override void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(meleePoint.position, meleePoint.TransformDirection(Vector3.forward), out hit, raycastRange, meleeLayerMask))
        {
            if(bonusHealth > 0)
            {
                hit.collider.GetComponent<Turret>().BeenHit(bonusHealth);
                bonusHealth = 0;
                BonusHealthBreak();
            } else if(bonusHealth <= 0)
            {
                hit.collider.GetComponent<Turret>().BeenHit(damage);
            }
        }
    }
}
