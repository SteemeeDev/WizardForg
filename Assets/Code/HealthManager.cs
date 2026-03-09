using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthManager : MonoBehaviour
{
    public int health = 100;
    
    float smoothTime = 1f;


    private void OnCollisionEnter(Collision collision)
    {
        NavMeshAgent enemyRb = collision.gameObject.GetComponent<NavMeshAgent>();

        IEnumerator Knockback()
        {
            float elapsed = 0f;

            while (elapsed < smoothTime)
            {
                elapsed += Time.deltaTime;
                
                enemyRb.Move((collision.transform.position - transform.position).normalized * 0.5f);
                yield return null;
            }
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("I got attacked");
            health += -10;
            
            Knockback();
            
        }
    }
}
