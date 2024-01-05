using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool isMoving;
    public bool isAttacking;
    public float attackCooldown;
    public float attackRate;
    public int damage;
    public float raycastRange;


    public LayerMask meleeLayerMask;
    public GameObject deathEffect;
    public EnemyDetector detector;
    public Transform meleePoint;
    public GameObject targetTurret;

    private void Start()
    {
        attackCooldown = attackRate;
    }
    private void Update()
    {
        if(isAttacking)
        {
            attackCooldown -= Time.deltaTime;
            if(attackCooldown <= 0f)
            {
                Attack();
                attackCooldown = attackRate;
            }
        }
    }

    private void FixedUpdate()
    {
        if(isAttacking)
        {
            RaycastHit hit;
            if (Physics.Raycast(meleePoint.position, meleePoint.TransformDirection(Vector3.forward), out hit, raycastRange, meleeLayerMask))
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
    }

    public void Hit(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Death();
        }
    }

    public void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(meleePoint.position, meleePoint.TransformDirection(Vector3.forward), out hit, raycastRange, meleeLayerMask))
        {
            hit.collider.GetComponent<Turret>().BeenHit(damage);
        }
    }

    public void Death()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
        Destroy(gameObject);
    }
}
