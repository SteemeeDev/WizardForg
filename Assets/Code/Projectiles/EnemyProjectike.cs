using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectike : Projectile
{
    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthManager>().TakeDamage(1);
            Destroy(gameObject);
        }else if (other.gameObject.layer == LayerMask.NameToLayer("3dEnvironment"))
        {
            Destroy(gameObject);
        }
    }
}
