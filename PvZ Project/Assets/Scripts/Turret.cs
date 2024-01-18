using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Turret : MonoBehaviour
{

    public float fireRate;
    public float fireCountdown = 0f;
    public int damage;
    public bool isWall;
    public bool isMelee;
    public bool targetInRange;
    public bool isAOE;  
    public int health;
    public int maxHealth;
    public bool isTalent;
    public float attackStartTime;
    public bool attacking;
    public float meleeAttackDistance = 2.5f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform meleePoint;
    public LayerMask meleeLayerMask;
    public MeleeDetector meleeDetector;
    public TurretBluePrint blueprint;
    public Talent talent;
    public Node node;
    public string attackSound;
    public string hitSound;
    public string deathSound;



    private void Start()
    {
        if(isMelee)
        {
            fireCountdown = 0f;
        }
    }

    public void Update()
    {
        if(!isWall && !isTalent)
        {
            if (fireCountdown >= fireRate && isMelee == false || fireCountdown >= fireRate && isMelee == true && targetInRange == true)
            {
                Shoot();
                fireCountdown = 0f;
                attacking = false;
            } else if(fireCountdown >= attackStartTime && !attacking)
            {
                PlaySound(attackSound);
                attacking = true;
            }
            fireCountdown += Time.deltaTime;
        } else if (isTalent)
        {

        }
    }

    public void Shoot()
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
                    PlaySound(hitSound);
                }
            }
            else
            {
                meleeDetector.attack = true;
                PlaySound(hitSound);
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

    public void Die()
    {
        node.tower = null;
        blueprint.spawnCount--;
        //  BuildManager.builtTurrets.Remove(this);
        PlaySound(deathSound);
        Destroy(gameObject);
    }

    public void PlaySound(string path)
    {
        RuntimeManager.PlayOneShot(path, Camera.main.transform.position);
    }
}
