using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool isMoving;
    public bool isAttacking;
    public float attackCooldown;
    public float attackRate;
    public int damage;
    public float raycastRange;
    public bool isShielded;
    public int bonusHealth;
    public bool isShield;
    public int shieldGiven;
    public ShieldingEnemy shield;


    public LayerMask meleeLayerMask;
    public LayerMask nodeLayerMask;
    public GameObject deathEffect;
    public EnemyDetector detector;
    public Transform meleePoint;
    public GameObject targetTurret;
    public GameObject parent;

    [Header("Audio")]
    public float attackSoundVolume;
    public float hitSoundVolume;
    public float deathSoundVolume;
    public string attackSound;
    public string hitSound;
    public string deathSound;

    private void Start()
    {
        attackCooldown = 0f;
    }
    public virtual void Update()
    {
        if(isAttacking)
        {
            attackCooldown += Time.deltaTime;
            if(attackCooldown >= attackRate)
            {
                Attack();
                attackCooldown = 0f;
            }
        }
    }

    public virtual void FixedUpdate()
    {
        if(isAttacking)
        {
            if (Physics.Raycast(meleePoint.position, meleePoint.TransformDirection(Vector3.forward), raycastRange, meleeLayerMask))
            {
                return;
            }
            else
            {
                March();
            }
        }
    }

    public virtual void March() {
        isAttacking = false;
        isMoving = true;
        attackCooldown = 0;
    }

    public virtual void Hit(int damage)
    {
        if(bonusHealth > 0)
        {
            bonusHealth -= damage;
            if (isShielded)
            {
                shieldGiven -= damage;
            }
            if(bonusHealth <= 0)
            {
                health += bonusHealth;
                bonusHealth = 0;
                BonusHealthBreak();
            } else if(isShielded && shieldGiven <= 0)
            {
                ShieldBreak();
            }
        } else if(bonusHealth <= 0)
        {
            health -= damage;
        }
        if(health <= 0)
        {
            Death();
        }
    }

    public virtual void BonusHealthBreak()
    {
        if (isShielded)
        {
            ShieldBreak();
        }
    }

    public void ShieldBreak()
    {
        isShielded = false;
        shield.Death();
        shield = null;
    }

    public virtual void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(meleePoint.position, meleePoint.TransformDirection(Vector3.forward), out hit, raycastRange, meleeLayerMask))
        {
            hit.collider.GetComponent<Turret>().BeenHit(damage);
        }
    }

    public virtual void Death()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        WaveSpawner.numOfAliveEnemies--;
        Destroy(effect, 3f);
        Destroy(parent);
    }
}
