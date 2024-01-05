using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Turret")
        {
            enemy.isAttacking = true;
            enemy.isMoving = false;
            enemy.targetTurret = other.gameObject;
        }
    }
}
