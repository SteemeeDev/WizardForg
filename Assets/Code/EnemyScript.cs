using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using Unity.VisualScripting;
using UnityEngine.LowLevel;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float AttackRange = 2.5f;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    int lockPos = 0;
    Transform playerPosition;
    private NavMeshAgent Agent;

    bool playerIsTarget = true;
   

    Vector3 targetPos = Vector3.zero;
   

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        playerPosition = PlayerController.Instance.transform;

        StartCoroutine(EnemyPathFinding());
    }

    Vector3 playerToEnemy;
    private void Update()
    {
        // locks the rotation so that the sprite doesn't fuck up :)
        transform.rotation = Quaternion.Euler(lockPos, lockPos, lockPos);

        playerToEnemy = transform.position - playerPosition.position;
        playerToEnemy = Quaternion.Euler(0, 45, 0) * playerToEnemy;

        float adjustedAtan = Mathf.Atan2(playerToEnemy.x, playerToEnemy.z) * (180f / Mathf.PI);

        animator.SetFloat("TurnDegrees", adjustedAtan);

        if (Mathf.Sign(adjustedAtan) == 1f)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        Debug.Log(Mathf.Atan2(Agent.velocity.x, Agent.velocity.z) * (180f / Mathf.PI));
    }

    virtual public IEnumerator EnemyPathFinding()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            if (playerIsTarget) Agent.SetDestination(playerPosition.position);
        }
    }

    IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("Hit player!");
            HealthManager plrHealth = collision.gameObject.GetComponent<HealthManager>();

            plrHealth.health -= 1;

            playerIsTarget = false;

            Agent.SetDestination((transform.position - playerPosition.position).normalized * 5);

            yield return new WaitForSeconds(0.5f);

            playerIsTarget = true;
        }

    }
    
}
