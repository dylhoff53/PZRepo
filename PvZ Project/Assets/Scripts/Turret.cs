using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;


    public void Update()
    {
        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
