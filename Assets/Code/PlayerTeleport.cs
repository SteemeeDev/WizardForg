
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] Transform TPTarget;
    

    [SerializeField] enemySpawn enemySpawner;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = TPTarget.position;
            enemySpawner.SpawnEnemies();
            gameObject.SetActive(false); 
            
        }
        

    }   
}
