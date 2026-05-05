using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectike : Projectile
{
    [SerializeField] Animator fireAnimator;
    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthManager>().TakeDamage(1);

            fireAnimator.SetBool("HitObject", true);
            yield return null;
            
            _rigidBody.velocity = Vector3.zero;

            yield return new WaitForSeconds(fireAnimator.GetCurrentAnimatorStateInfo(0).length);

            Destroy(gameObject);

        }else if (other.gameObject.layer == LayerMask.NameToLayer("3dEnvironment"))
        {
            fireAnimator.SetBool("HitObject", true);
            yield return null;

            _rigidBody.velocity = Vector3.zero;

            yield return new WaitForSeconds(fireAnimator.GetCurrentAnimatorStateInfo(0).length);

            Destroy(gameObject);
        }
    }
}
