using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using Unity.VisualScripting;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float AttackRange = 2.5f;
    int lockPos = 0;
    Transform playerPosition;
    private NavMeshAgent Agent;
   


   

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        playerPosition = PlayerController.Instance.transform;

        StartCoroutine(EnemyPathFinding());
    }

    private void Update()
    {
        // locks the rotation so that the sprite doesn't fuck up :)
        transform.rotation = Quaternion.Euler(lockPos, lockPos, lockPos);
        
    }

    IEnumerator EnemyPathFinding()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            Agent.SetDestination(playerPosition.position);

        }
    }

    /* This shit works
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("I attack player");
        }
    }
    */
}
