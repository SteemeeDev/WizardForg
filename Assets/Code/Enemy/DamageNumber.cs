using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    public TMP_Text text;
    [SerializeField] float lifeTime = 3f;
    float timeAlive;
    Rigidbody rb;

    float fallSpeed = 3f;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(-3f, 3f), 2, Random.Range(-3f, 3f));
    }

    private void Update()
    {
        rb.velocity -= Vector3.up * Time.deltaTime * Time.deltaTime * fallSpeed;

        timeAlive += Time.deltaTime;
        if (timeAlive > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
