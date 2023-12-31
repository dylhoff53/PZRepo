using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] spawns;

    [SerializeField]
    public EnemyType[] typesOfEnemies;

    public float timeBetweenWaves;
    private float countdown = 2f;

    public TextMeshProUGUI waveCountdownText;

    public float[] spawnInters;
    public Queue<float> spawnTimes = new Queue<float>();
    public float spawnTimer;
    public bool outOfEnemies;

    private int waveIndex = 0;

    public float[] weights;
    public float[] enemyWeights;

    private void Start()
    {
        for (int i = 0; i < spawnInters.Length; i++)
        {
            spawnTimes.Enqueue(spawnInters[i]);
        }

        /*
        float baseEnemyWeight = 1f / typesOfEnemies.Length;
        enemyWeights = new float[typesOfEnemies.Length];
        Debug.Log(baseEnemyWeight);
        for (int j = 0; j < enemyWeights.Length; j++)
        {
            enemyWeights[j] = baseEnemyWeight;
        }
        */
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
        float enemyChoice = Random.Range(0f, 1f);

        spawnNumber = Mathf.Round(spawnNumber * 100f) / 100f;
        enemyChoice = Mathf.Round(enemyChoice * 100f) / 100f;
        Debug.Log("Spawn Number" + spawnNumber);
        Debug.Log("Enemy Choice" + enemyChoice);
        int spawnLane = PercentCheck(spawnNumber, weights);
        int chosenEnemyType = PercentCheck(enemyChoice, enemyWeights);

        UpdateWeights(spawnLane, typesOfEnemies[chosenEnemyType].laneWeight, weights);
        UpdateWeights(chosenEnemyType, typesOfEnemies[chosenEnemyType].weight, enemyWeights);
        GameObject enemy = Instantiate(typesOfEnemies[chosenEnemyType].enemyPrefab, spawns[spawnLane].GetComponent<Lane>().spawn.position, spawns[spawnLane].GetComponent<Lane>().spawn.rotation);
        enemy.GetComponent<EnemyMovement>().target = spawns[spawnLane].GetComponent<Lane>().target;
    }


    public int PercentCheck(float chosenNumber, float[] weightArray)
    {
        int calcLane = 0;
        float valueChecker = 0f;
        for (int i = 0; i < weightArray.Length; i++)
        {
            valueChecker += weightArray[i];
            if (chosenNumber <= valueChecker)
            {
                calcLane = i;
                return calcLane;
            }
        }
        return 0;
    }


    public void UpdateWeights(int lane, float weightChange, float[] weightArray)
    {
        for(int i = 0; i < weightArray.Length; i++)
        {
            if(lane == i)
            {
                weightArray[i] -= weightChange;
            }else
            {
                weightArray[i] += weightChange / 4;
            }
        }
    }
}
