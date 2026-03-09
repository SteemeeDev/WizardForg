using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float AttackRange = 2.5f;
    [SerializeField] float MovementSpeed = 1.0f;
    int lockPos = 1;
    Transform playerPosition;
    private NavMeshAgent Agent; 

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        playerPosition = PlayerController.Instance.transform;
        Agent.SetDestination(playerPosition.position);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(lockPos, lockPos, lockPos);
    }

    void EnemyPathFinding()
    {
        
    }
}
