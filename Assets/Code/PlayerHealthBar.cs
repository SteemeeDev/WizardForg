using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Image[] hearts;
    [SerializeField] PlayerHealthManager healthManager;

    private void Start()
    {
        StartCoroutine(shineHearts());
    }

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].color = new Color(1f, 1f, 1f, 0.2f);
        }

        for (int i = 0; i < healthManager.health; i++)
        {
            hearts[i].color = new Color(1f, 1f, 1f, 1f);
        }
    }


    IEnumerator shineHearts()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));

            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].GetComponent<Animator>().SetTrigger("Shine");
            }
        }
    }
}
