using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;
    [SerializeField] Animator playerAnimator;
    [SerializeField] float iFrames = 0.5f;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject deathScreen;
    [SerializeField] SpriteRenderer playerRenderer;

    [SerializeField] GameObject damageNumberPrefab;

    Coroutine IframeRoutine;

    bool canTakeDamage = true;
    public bool playerIsDead = false;

    public void TakeDamage(int damage)
    {
        if (health > 0 && canTakeDamage)
        {
            health -= damage;

            if (IframeRoutine != null) StopCoroutine(IframeRoutine);
            IframeRoutine = StartCoroutine(InvincibilityFrames());

            healthBar.GetComponent<PlayerHealthBar>().UpdateHealth();

            GameObject damageNr = Instantiate(damageNumberPrefab, transform.position, Quaternion.identity);
            damageNr.transform.position = transform.position;
            DamageNumber damageNumberManager = damageNr.GetComponent<DamageNumber>();
            damageNumberManager.damageAmount = damage;
            damageNumberManager.SpawnObject(transform.position);
        }

        if (health <= 0 && !playerIsDead)
        {
            StopAllCoroutines();

            playerRenderer.color = Color.white;

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
            plrController.wandManager.currentWand.gameObject.SetActive(false);
            plrController.wandManager.gameObject.SetActive(false);



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
        // Fuck this shit im hardcoding the animation
        int greyScalePulses = 3;

        if (!playerIsDead)
        {
            for (int i = 0; i < greyScalePulses; i++)
            {
                yield return new WaitForSeconds(iFrames / greyScalePulses * 0.3f);
                playerRenderer.color = Color.white * 0.8f;
                yield return new WaitForSeconds(iFrames / greyScalePulses * 0.7f);
                playerRenderer.color = Color.white;
            }
        }
        else
        {
            playerRenderer.color = Color.white;
        }




        canTakeDamage = true;
    }
    

}
