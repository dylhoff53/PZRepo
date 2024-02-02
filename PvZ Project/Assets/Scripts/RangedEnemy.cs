using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public override void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(detector.transform.position, detector.transform.TransformDirection(Vector3.forward), out hit, raycastRange, meleeLayerMask))
        {
            return;
        }
        else
        {
            isAttacking = false;
            isMoving = true;
            attackCooldown = attackRate;
        }
    }

    public override void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        bullet.GetComponent<EnemyBullet>().damage = damage;
        bullet.GetComponent<EnemyBullet>().enemy = this;
    }
}
