using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] WaveConfig currentWave;
    [SerializeField] bool isLooping = false;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        do{
            foreach (WaveConfig waveConfig in waveConfigs)
        {
            currentWave = waveConfig;
            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWaypoint().position, Quaternion.Euler(0,0,180), transform);
                yield return new WaitForSeconds(currentWave.GetrandomSpawnTime());
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
        }
        while(isLooping);
        

    }
    public WaveConfig GetCurrentWave()
    {
        return currentWave;
    }
}
