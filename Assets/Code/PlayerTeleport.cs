
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] Transform playerTeleportTarget;
    [SerializeField] enemySpawn enemySpawner;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = playerTeleportTarget.position;
            enemySpawner.SpawnEnemies();
        }
    }
}
