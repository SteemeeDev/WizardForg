using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveWand : MonoBehaviour
{
    [SerializeField] WandManager.Wand wandToGive;
    [SerializeField] WandManager wandManager;
    [SerializeField] Animator animator;
    [SerializeField] GameObject wandObject;

    [SerializeField] enemySpawn enemySpawner;
    [SerializeField] Transform tpTarget;

    bool hasGivenWand;

    private IEnumerator OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !hasGivenWand)
        {
            hasGivenWand = true;
            animator.SetBool("Open", true);
            yield return new WaitForSeconds(4f);
            wandManager.UnluckWand(wandToGive);
            wandObject.SetActive(false);

            yield return new WaitForSeconds(5f);
            if (tpTarget != null) other.gameObject.GetComponent<Rigidbody>().position = tpTarget.position;
            if (enemySpawner != null) enemySpawner.StartCoroutine(enemySpawner.SpawnEnemies());
        }
    }
}
