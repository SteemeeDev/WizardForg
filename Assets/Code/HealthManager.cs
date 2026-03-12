using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthManager : MonoBehaviour
{

    public int health = 10;
    
   

    void UpdateHealthBar()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        NavMeshAgent enemyRb = collision.gameObject.GetComponent<NavMeshAgent>();

        if (collision.gameObject.tag == "Enemy")
        {
           // Debug.Log("I got attacked");
            health += -10;
            
        }
    }
}
