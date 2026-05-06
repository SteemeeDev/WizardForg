using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeParticle : MonoBehaviour
{
    [SerializeField] float lifeTime;
    float timeAlive = 0;

    private void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
