using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] GameObject heartSprite;
    [SerializeField] PlayerHealthManager healthManager;
    List<Heart> hearts = new List<Heart>();

    private void Start()
    {
        UpdateHealth();
        StartCoroutine(IEShineHearts());
    }


    public void UpdateHealth()
    {
        hearts.Clear();
        for (int i = 0; i < healthManager.maxHealth; i++)
        {
            GameObject heartObject = Instantiate(heartSprite, transform);
            Heart heart = heartObject.GetComponent<Heart>();
            hearts.Add(heart);

            if (i >= healthManager.health)
            {
                hearts[i].GetComponent<Animator>().SetBool("Alive", false);
                heart.alive = false;
            }
            else
            {
                hearts[i].GetComponent<Animator>().SetBool("Alive", true);
                heart.alive = true;
            }
        }
    }

    IEnumerator IEShineHearts()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));

            for (int i = 0; i < hearts.Count; i++)
            {
                hearts[i].GetComponent<Animator>().SetTrigger("Shine");
            }
        }
    }
}
