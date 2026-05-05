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
    public SpriteRenderer spriteRenderer;
    int lockPos = 0;
    public Transform playerPosition;
    public NavMeshAgent Agent;

    bool playerIsTarget = true;
   

    public Vector3 targetPos = Vector3.zero;
   

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        playerPosition = PlayerController.Instance.transform;
        StartCoroutine(EnemyPathFinding());
    }
    
    public Vector3 enemyToPlayer;

    public virtual void Update()
    {
        if (playerPosition == null)
        {
            playerPosition = PlayerController.Instance.transform;
        }
        // locks the rotation so that the sprite doesn't fuck up :)
        transform.rotation = Quaternion.Euler(lockPos, lockPos, lockPos);

        enemyToPlayer = playerPosition.position - transform.position;
        enemyToPlayer = Quaternion.Euler(0, 135, 0) * enemyToPlayer;
        //Debug.DrawRay(transform.position, enemyToPlayer);

        float adjustedAtan = Mathf.Atan2(enemyToPlayer.x, enemyToPlayer.z) * (180f / Mathf.PI);
       // Debug.Log(adjustedAtan);

        animator.SetFloat("TurnDegrees", adjustedAtan);

        if (Mathf.Sign(adjustedAtan) == -1f)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }


    }

    virtual public IEnumerator EnemyPathFinding()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            if (playerIsTarget && playerPosition != null) Agent.SetDestination(playerPosition.position);
        }
    }

    IEnumerator OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player!");
            PlayerHealthManager plrHealth = collider.gameObject.GetComponent<PlayerHealthManager>();

            plrHealth.TakeDamage(1);

            playerIsTarget = false;

            Agent.SetDestination(playerPosition.position + (transform.position - playerPosition.position).normalized * 1.3f);

            yield return new WaitForSeconds(0.3f);

            playerIsTarget = true;
        }

    }

    private void OnDestroy()
    {
        Agent.enabled = false;
    }

}
