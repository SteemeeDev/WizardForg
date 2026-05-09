
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] EnemyScript enemyScript;
    [SerializeField] GameObject damageNumberPrefab;
    public enemySpawn _enemySpawn;
    public float health = 100f;

    float timeSinceLastHit = 999f;
    float damageAnimationTime = 0.15f;

    [SerializeField] AudioSource damageAudioSource;
    [SerializeField] AudioClip damageSound;

    Color originalSpriteColor;
    
    public void TakeDamage(float damage)
    {
        timeSinceLastHit = 0;
        health -= damage;
        if (health <= 0f)
        {
            if(_enemySpawn != null) _enemySpawn.enemyList.Remove(gameObject);
          //  Debug.Log(_enemySpawn.enemyList.Count);
            Destroy(gameObject);
        }

        GameObject damageNr = Instantiate(damageNumberPrefab, transform.position, Quaternion.identity);
        damageNr.transform.position = transform.position;
        DamageNumber damageNumberManager = damageNr.GetComponent<DamageNumber>();
        damageNumberManager.damageAmount = damage;
        damageNumberManager.SpawnObject(transform.position);

        damageAudioSource.PlayOneShot(damageSound);
    }

    private float originalSpeed;
    private void Start()
    {
        originalSpriteColor = enemyScript.spriteRenderer.color;
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
            enemyScript.spriteRenderer.color = originalSpriteColor;
            enemyScript.Agent.speed = originalSpeed;
        }
    }

}
