using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Turret : MonoBehaviour
{

    [Header("Turret Identifiers")]
    public bool isWall;
    public bool isMelee;
    public bool targetInRange;
    public bool isAOE;
    public bool isTalent;

    [Header("Basic Stats")]
    public float fireRate;
    public float fireCountdown = 0f;
    public int damage;
    public int health;
    public int maxHealth;
    public float attackDelay;

    [Header("Ranged Stats")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Melee Stats")]
    public Transform meleePoint;
    public LayerMask meleeLayerMask;
    public MeleeDetector meleeDetector;
    public float meleeAttackDistance = 2.5f;

    [Header("Audio")]
    public float attackSoundVolume;
    public float hitSoundVolume;
    public float deathSoundVolume;
    public string attackSound;
    public string hitSound;
    public string deathSound;

    public TurretBluePrint blueprint;
    public Node node;



    private void Start()
    {
        if(isMelee)
        {
            fireCountdown = 0f;
        }
    }

    public virtual void Update()
    {
        if(!isWall)
        {
            if (fireCountdown >= fireRate && isMelee == false || fireCountdown >= fireRate && isMelee == true && targetInRange == true)
            {
                fireCountdown = 0f;
                AudioManager.PlayOneShot(attackSound, attackSoundVolume, AudioManager.location);
                Invoke("Shoot", attackDelay);
            }
            fireCountdown += Time.deltaTime;
        }
    }

    public virtual void Shoot()
    {
        if(!isMelee)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            bullet.GetComponent<Bullet>().damage = damage;
            bullet.GetComponent<Bullet>().turret = this;

        }
        else
        {
            if(!isAOE)
            {
                RaycastHit hit;
                if (Physics.Raycast(meleePoint.position, meleePoint.TransformDirection(Vector3.forward), out hit, meleeAttackDistance, meleeLayerMask))
                {
                    hit.collider.GetComponent<Enemy>().Hit(damage);
                    AudioManager.PlayOneShot(hitSound, hitSoundVolume, AudioManager.location);
                }
            }
            else
            {
                meleeDetector.attack = true;
                AudioManager.PlayOneShot(hitSound, hitSoundVolume, AudioManager.location);
            }
        }
    }

    public void BeenHit(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        node.tower = null;
        blueprint.spawnCount--;
        AudioManager.PlayOneShot(deathSound, deathSoundVolume, AudioManager.location);
        Destroy(gameObject);
    }
}
