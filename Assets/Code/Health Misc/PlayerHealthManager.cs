using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;
    [SerializeField] WandController wandController;
    [SerializeField] Animator playerAnimator;
    [SerializeField] float iFrames = 0.5f;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject deathScreen;
    

    bool canTakeDamage = true;
    public bool playerIsDead = false;

    public void TakeDamage(int damage)
    {
        if (health > 0 && canTakeDamage)
        {
            health -= damage;
            StartCoroutine(InvincibilityFrames());
            healthBar.GetComponent<PlayerHealthBar>().UpdateHealth();
        }

        if (health <= 0 && !playerIsDead)
        {
            playerIsDead = true;

            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject go in enemy)
            {
                Destroy(go);
            }

            healthBar.SetActive(false);
            

            //disables player movement and wand
            PlayerController plrController = GetComponent<PlayerController>();
            plrController.enabled = false;
            plrController.currentWand.gameObject.SetActive(false);

            

            playerAnimator.SetTrigger("Die");
        }
    }

    // Not used as of now
    public void HealPlayer(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        healthBar.GetComponent<PlayerHealthBar>().UpdateHealth();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Brugertest");
        }
    }

    IEnumerator InvincibilityFrames()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(iFrames);
        canTakeDamage = true;
    }
    

}
