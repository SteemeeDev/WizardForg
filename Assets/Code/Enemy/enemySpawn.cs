using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    [SerializeField] Transform[] enemySpawnPositions;
    [SerializeField] Wave wave;

    private int spawnPos;

    public List<GameObject> enemyList;

    public IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < wave.enemies.Length; i++)
        {
            spawnPos = Random.Range(0, enemySpawnPositions.Length);
            if (enemySpawnPositions != null)
            {
                yield return new WaitForSeconds(3f);
                GameObject enemy = Instantiate(wave.enemies[i], enemySpawnPositions[spawnPos].position, enemySpawnPositions[spawnPos].rotation);
                enemy.GetComponent<EnemyHealth>()._enemySpawn = this;
                enemyList.Add(enemy);
            }
        }
    }


    private void Update()
    {
        
    }
}
