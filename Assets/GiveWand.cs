using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveWand : MonoBehaviour
{
    [SerializeField] WandManager.Wand wandToGive;
    [SerializeField] WandManager wandManager;
    [SerializeField] Animator animator;
    [SerializeField] GameObject wandObject;


    private IEnumerator OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Open", true);
            yield return new WaitForSeconds(4f);
            wandManager.UnluckWand(wandToGive);
            wandObject.SetActive(false);
        }
    }
}
