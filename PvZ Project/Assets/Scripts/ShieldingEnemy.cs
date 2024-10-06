using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldingEnemy : Enemy
{
    public GameObject shieldEffect;
    public Enemy host;
    public Vector3 shieldOffset;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy" && collision.collider.GetComponent<Enemy>().isShielded == false && collision.collider.GetComponent<Enemy>().isShield == false)
        {
            host = collision.collider.GetComponent<Enemy>();
            host.isShielded = true;
            host.shield = this;
            host.bonusHealth += health;
            transform.parent = host.parent.transform;
            host.shieldGiven = health;
            transform.position = new Vector3 (host.transform.position.x + shieldOffset.x, host.transform.position.y + shieldOffset.y, host.transform.position.z + shieldOffset.z);
            meleePoint.position = host.meleePoint.position;
            detector.transform.position = host.detector.transform.position;
            Destroy(parent);
        }
    }

    public override void Hit(int damage)
    {
        if (host == null)
        {
            if (bonusHealth > 0)
            {
                bonusHealth -= damage;
                if (bonusHealth <= 0)
                {
                    health += bonusHealth;
                    bonusHealth = 0;
                    BonusHealthBreak();
                }
            }
            else if (bonusHealth <= 0)
            {
                health -= damage;
            }
            if (health <= 0)
            {
                Death();
            }
        }
        else
        {
            //host.Hit(damage);
        }
    }

    public override void Death()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        WaveSpawner.numOfAliveEnemies--;
        Destroy(effect, 3f);
        if(host != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(parent);
        }
    }
}
