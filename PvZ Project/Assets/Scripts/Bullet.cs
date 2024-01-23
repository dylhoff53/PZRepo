using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float maxLife;
    public float lifeCounter = 0f;
    public GameObject impactEffect;
    public float explosionRadius = 0f;
    public int damage;
    public Turret turret;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        lifeCounter += Time.deltaTime;
        if(lifeCounter >= maxLife)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (explosionRadius > 0f)
        {
            Explode();
        }else
        {
            if (other.tag == "Enemy")
            {
                Debug.Log("Hit");
                GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(effectIns, 2f);
                other.GetComponent<Enemy>().Hit(damage);
                AudioManager.PlayOneShot(turret.hitSound, turret.hitSoundVolume, AudioManager.location);
                Destroy(gameObject);
            }
        }
    }

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().Hit(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
