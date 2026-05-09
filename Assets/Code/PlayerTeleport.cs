
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] Transform TPTarget;
    [SerializeField] enemySpawn enemySpawner;

    public bool hasTped;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasTped)
        {
            hasTped = true;
            if (TPTarget != null) other.transform.position = TPTarget.position;
            enemySpawner.StartCoroutine(enemySpawner.SpawnEnemies());
            gameObject.SetActive(false); 
        }
    }   
}
