using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] EnemyScript enemyScript;

    public float health = 100f;

    float timeSinceLastHit = 999f;
    float damageAnimationTime = 0.15f;

    public void TakeDamage(float damage)
    {
        timeSinceLastHit = 0;
        health -= damage;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private float originalSpeed;
    private void Start()
    {
        originalSpeed = enemyScript.Agent.speed;
    }

    private void Update()
    {
        timeSinceLastHit += Time.deltaTime;
        if (timeSinceLastHit < damageAnimationTime)
        {
            enemyScript.Agent.speed = originalSpeed * 0.5f;
            enemyScript.spriteRenderer.color = new Color(0.7f, 0, 0);
        }
        else
        {
            enemyScript.spriteRenderer.color = Color.white;
            enemyScript.Agent.speed = originalSpeed;
        }
    }

}
