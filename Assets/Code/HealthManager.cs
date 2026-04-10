using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class HealthManager : MonoBehaviour
{

    public int health = 10;
    public GameObject[] enemy;
    [SerializeField]  WandController wandController;
    [SerializeField] Animator animator;


    private void Update()
    {
        if (health == 0)
        {
            health = -1;

            enemy = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject go in enemy)
            {
                Destroy(go);
                
                if (go != null)
                {
                    break;
                }
            }

            //disables player movement and wand
            GetComponent<PlayerController>().enabled = false;
            wandController.gameObject.SetActive(false);

            
            animator.SetTrigger("Die");
            
        }
        
    }
}
