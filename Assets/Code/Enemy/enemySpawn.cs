
using UnityEngine;

public class enemySpawn : MonoBehaviour
{


    [SerializeField] Transform[] enemySpawnPositions;
    public GameObject meleeEnemy;
    public GameObject rangedEnemy;

    private int spawnPos;

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemySpawnPositions.Length; i++)
        {
            spawnPos = Random.Range(0, enemySpawnPositions.Length);
            GameObject enemy = Instantiate(meleeEnemy, enemySpawnPositions[spawnPos].position, enemySpawnPositions[spawnPos].rotation);
        }
    }



}
