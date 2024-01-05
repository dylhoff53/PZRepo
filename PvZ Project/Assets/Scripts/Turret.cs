using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform meleePoint;
    public LayerMask meleeLayerMask;
    public MeleeDetector meleeDetector;
    public Node node;

    private void Start()
    {
        if(isMelee)
        {
            fireCountdown = 0f;
        }
    }

    public void Update()
    {
        if(!isWall)
        {
            if (fireCountdown <= 0f && isMelee == false || fireCountdown <= 0f && isMelee == true && targetInRange == true)
            {
                Shoot();
                fireCountdown = fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if(!isMelee)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            bullet.GetComponent<Bullet>().damage = damage;
        }
        else
        {
            if(!isAOE)
            {
                RaycastHit hit;
                if (Physics.Raycast(meleePoint.position, meleePoint.TransformDirection(Vector3.forward), out hit, 2.5f, meleeLayerMask))
                {
                    hit.collider.GetComponent<Enemy>().Hit(damage);
                }
            }
            else
            {
                meleeDetector.attack = true;
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
        Destroy(gameObject);
    }
}
