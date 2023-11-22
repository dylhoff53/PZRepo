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

    private int waveIndex = 0;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave()); 
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Round(countdown).ToString();
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
        int spawner = Random.Range(0, 5);
        Transform enemy = Instantiate(enemyPrefab, spawns[spawner].GetComponent<Lane>().spawn.position, spawns[spawner].GetComponent<Lane>().spawn.rotation);
        enemy.GetComponent<EnemyMovement>().target = spawns[spawner].GetComponent<Lane>().target;
    }
}
