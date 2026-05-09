using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    [SerializeField] Transform[] enemySpawnPositions;
    [SerializeField] Wave[] waves;

    public bool spawnedAllEnemies;

    private int spawnPos;

    public List<GameObject> enemyList;

    [SerializeField] bool loopForever;

    public IEnumerator SpawnEnemies()
    {
        for (int waveIndex = 0; waveIndex < waves.Length; waveIndex++)
        {
            yield return new WaitForSeconds(waves[waveIndex].waveDelay);
            for (int enemyIndex = 0; enemyIndex < waves[waveIndex].enemies.Length; enemyIndex++)
            {
                spawnPos = Random.Range(0, enemySpawnPositions.Length);
                if (enemySpawnPositions != null)
                {
                    yield return new WaitForSeconds(waves[waveIndex].delayBetweenEnemySpawns);
                    GameObject enemy = Instantiate(waves[waveIndex].enemies[enemyIndex], enemySpawnPositions[spawnPos].position, enemySpawnPositions[spawnPos].rotation);
                    enemy.GetComponent<EnemyHealth>()._enemySpawn = this;
                    enemyList.Add(enemy);
                }
            }
        }

        spawnedAllEnemies = true;
    }


    private void Update()
    {
        if (spawnedAllEnemies == true && enemyList.Count == 0 && loopForever) StartCoroutine(SpawnEnemies());
    }
}
