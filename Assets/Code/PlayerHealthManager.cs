using System.Collections;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int health = 10;
    [SerializeField] WandController wandController;
    [SerializeField] Animator playerAnimator;
    [SerializeField] float iFrames = 0.5f;

    bool canTakeDamage = true;
    bool playerIsDead = false;

    public void TakeDamage(int damage)
    {
        if (health > 0 && canTakeDamage)
        {
            health -= damage;
            StartCoroutine(InvincibilityFrames());
        }
        else if (health <= 0 && !playerIsDead)
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

    IEnumerator InvincibilityFrames()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(iFrames);
        canTakeDamage = true;
    }
}
