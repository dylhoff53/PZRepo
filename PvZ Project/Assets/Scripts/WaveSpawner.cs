using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] spawns;
    public static WaveSpawner instance;

    [SerializeField]
    public EnemyType[] typesOfEnemies;

    public float timeBetweenWaves;

    public SpawnableEvent[] spawnInters;
    public Queue<SpawnableEvent> spawnTimes = new Queue<SpawnableEvent>();
    public float spawnTimer;
    public bool outOfEnemies;

    public float[] weights;
    public float[] enemyWeights;
    public static int numOfAliveEnemies;

    public int points;
    public float pointTimer;
    public float pointGainInterval;
    public bool canGetPoints;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in Scene!");
            return;
        }
        instance = this;
    }

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
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnTimes.Peek().spawnTime)
            {
                if(points > 0)
                {
                    if (!spawnTimes.Peek().isEvent)
                    {
                        SpawnEnemy();
                    }
                    else
                    {
                        spawnTimes.Peek().DoEvent();
                    }
                }
                spawnTimes.Dequeue();
                spawnTimer = 0f;
            }
            if (canGetPoints)
            {
                pointTimer += Time.deltaTime;
                if(pointTimer >= pointGainInterval)
                {
                    pointTimer = 0f;
                    points++;
                }
            }
        } else if(spawnTimes.Count <= 0 && numOfAliveEnemies <= 0 && SceneMan.died == false)
        {
            canGetPoints = false;
            outOfEnemies = true;
            SceneMan.win = true;
        }
    }

    void SpawnEnemy()
    {

        float spawnNumber = Random.Range(0f, 1f);
        //float enemyChoice = Random.Range(0f, 1f);

        spawnNumber = Mathf.Round(spawnNumber * 100f) / 100f;
        //enemyChoice = Mathf.Round(enemyChoice * 100f) / 100f;
        Debug.Log("Spawn Number" + spawnNumber);
        //Debug.Log("Enemy Choice" + enemyChoice);
        //int chosenEnemyType = PercentCheck(enemyChoice, enemyWeights);
        int chosenEnemyType = PointsCheck();
        int spawnLane = PercentCheck(spawnNumber, weights);

        UpdateWeights(spawnLane, typesOfEnemies[chosenEnemyType].laneWeight, weights);
        UpdateWeights(chosenEnemyType, typesOfEnemies[chosenEnemyType].weight, enemyWeights);
        numOfAliveEnemies++;
        GameObject enemy = Instantiate(typesOfEnemies[chosenEnemyType].enemyPrefab, spawns[spawnLane].GetComponent<Lane>().spawn.position, spawns[spawnLane].GetComponent<Lane>().spawn.rotation);
        enemy.GetComponent<EnemyMovement>().target = spawns[spawnLane].GetComponent<Lane>().target;
    }

    public int PointsCheck()
    {
        int enemyType = 0;
        bool validSpawn = false;
        /*
        EnemyType[] validEnemies;
        foreach (EnemyType enemy in typesOfEnemies)
        {
            if (enemy.cost )
        } */
        while (!validSpawn)
        {
            float enemyChoice = Random.Range(0f, 1f);
            enemyChoice = Mathf.Round(enemyChoice * 100f) / 100f;
            enemyType = PercentCheck(enemyChoice, enemyWeights);
            if (typesOfEnemies[enemyType].cost > points)
            {
                continue;
            }
            else
            {
                validSpawn = true;
                points -= typesOfEnemies[enemyType].cost;
            }
        }
        return enemyType;
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
