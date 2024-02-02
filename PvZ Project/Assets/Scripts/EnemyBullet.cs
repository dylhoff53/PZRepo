using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float maxLife;
    public float lifeCounter = 0f;
    public GameObject impactEffect;
    public float explosionRadius = 0f;
    public int damage;
    public Enemy enemy;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        lifeCounter += Time.deltaTime;
        if (lifeCounter >= maxLife)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Turret")
        {
            Debug.Log("Hit");
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
            other.GetComponent<Turret>().BeenHit(damage);
            AudioManager.PlayOneShot(enemy.hitSound, enemy.hitSoundVolume, AudioManager.location);
            Destroy(gameObject);
        }
        
    }
}
