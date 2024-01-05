using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform target;
    public Enemy enemy;


    private void Update()
    {
        if(enemy.isMoving)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.3f)
            {
                Destroy(gameObject);
                Debug.Log("Die!");
            }
        }
    }
}
