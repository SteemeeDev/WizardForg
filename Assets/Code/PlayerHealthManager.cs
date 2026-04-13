using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public int health = 10;
    [SerializeField] WandController wandController;
    [SerializeField] Animator playerAnimator;
    [SerializeField] float iFrames = 0.5f;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject deathScreen;

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

            healthBar.SetActive(false);
            deathScreen.SetActive(true);

            //disables player movement and wand
            PlayerController plrController = GetComponent<PlayerController>();
            plrController.enabled = false;
            plrController.currentWand.gameObject.SetActive(false);


            playerAnimator.SetTrigger("Die");

        }
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
