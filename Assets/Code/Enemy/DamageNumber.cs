using System.Collections;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] TMP_Text damageText;
    [SerializeField] Rigidbody rb;

    public float damageAmount;

    [SerializeField] float lifeTime = 3f;
    [SerializeField] float fallSpeed = 3f;

    [SerializeField] Color maxDamageColor;
    [SerializeField] Color mediumDamageColor;


    float timeAlive;
    public void SpawnObject(Vector3 spawnPos)
    {
        damageText.text = ((int)damageAmount).ToString();

        if (damageAmount > 35f) damageText.color = maxDamageColor;
        else if (damageAmount > 15f) damageText.color = mediumDamageColor;
        else damageText.color = Color.white;

        transform.position = spawnPos;

        rb.velocity = new Vector3(Random.Range(-3f, 3f), 2, Random.Range(-3f, 3f));

        StartCoroutine(IEUpdate());
    }


    IEnumerator IEUpdate()
    {
        while (true)
        {   
            rb.velocity -= Vector3.up * Time.deltaTime * Time.deltaTime * fallSpeed;

            timeAlive += Time.deltaTime;
            if (timeAlive > lifeTime)
            {
                Destroy(gameObject);
            }

            yield return null;
        }
    }
}
