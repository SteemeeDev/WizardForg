using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int health = 10;
    [SerializeField] WandController wandController;
    [SerializeField] Animator playerAnimator;

    bool playerIsDead = false;

    private void Update()
    {
        if (health <= 0 && !playerIsDead)
        {
            playerIsDead = true;

            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject go in enemy)
            {
                Destroy(go);
            }

            //disables player movement and wand
            GetComponent<PlayerController>().enabled = false;
            wandController.gameObject.SetActive(false);

            
            playerAnimator.SetTrigger("Die");
            
        }
        
    }
}
