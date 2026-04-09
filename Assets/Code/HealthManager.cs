using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthManager : MonoBehaviour
{

    public int health = 10;
    [SerializeField]  WandController wandController;
    [SerializeField] Animator animator;

    private void Update()
    {
        if (health == 0)
        {
            //Stops time and disables player movement and wand
            GetComponent<PlayerController>().enabled = false;
            wandController.gameObject.SetActive(false); 
            Time.timeScale = 0f;

            animator.SetTrigger("Die");
            
        }
        
    }
}
