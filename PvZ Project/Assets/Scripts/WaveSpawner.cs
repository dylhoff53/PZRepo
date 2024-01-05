using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform[] spawns;

    public float timeBetweenWaves;
    private float countdown = 2f;

    public TextMeshProUGUI waveCountdownText;

    public float[] spawnInters;
    public Queue<float> spawnTimes = new Queue<float>();
    public float spawnTimer;
    public bool outOfEnemies;

    private int waveIndex = 0;

    public float[] weights;

    private void Start()
    {
        for (int i = 0; i < spawnInters.Length; i++)
        {
            spawnTimes.Enqueue(spawnInters[i]);
        }
    }

    void Update()
    {

        if(spawnTimes.Count != 0)
        {
            if (spawnTimer >= spawnTimes.Peek())
            {
                spawnTimes.Dequeue();
                spawnTimer = 0f;
                SpawnEnemy();
            }

            spawnTimer += Time.deltaTime;
        } else
        {
            outOfEnemies = true;
        }


        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    { 
        float spawnNumber = Random.Range(0f, 1f);
        spawnNumber = Mathf.Round(spawnNumber * 100f) / 100f;
        Debug.Log(spawnNumber);
        int spawnLane = LaneCheck(spawnNumber);

        UpdateWeights(spawnLane, 0.1f);
        Transform enemy = Instantiate(enemyPrefab, spawns[spawnLane].GetComponent<Lane>().spawn.position, spawns[spawnLane].GetComponent<Lane>().spawn.rotation);
        enemy.GetComponent<EnemyMovement>().target = spawns[spawnLane].GetComponent<Lane>().target;
    }


    public int LaneCheck(float chosenNumber)
    {
        int calcLane = 0;
        float valueChecker = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            valueChecker += weights[i];
            if (chosenNumber <= valueChecker)
            {
                calcLane = i;
                return calcLane;
            }
        }
        return 0;
    }


    public void UpdateWeights(int lane, float weightChange)
    {
        for(int i = 0; i < weights.Length; i++)
        {
            if(lane == i)
            {
                weights[i] -= weightChange;
            }else
            {
                weights[i] += weightChange / 4;
            }
        }
    }
}
