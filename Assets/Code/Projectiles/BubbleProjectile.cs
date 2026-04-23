using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class BubbleProjectile : Projectile
{
    public float chargeUpTime = 2f;
    public float charge = 0f;

    bool fired = false;

    // Debounce variable that triggers the onShoot
    bool onFired = false;


    List<GameObject> hitEnemies = new List<GameObject>();

    Vector3 travelDir = Vector3.zero;
    public override IEnumerator FireProjectile(WandController controller, Transform startPos)
    {
        while (timeAlive < lifetime)
        {
            timeAlive += Time.deltaTime;

            if (Input.GetMouseButton(0) && !fired && charge <= chargeUpTime && controller != null)
            {
                transform.position = new Vector3(
                    startPos.position.x,
                    PlayerController.Instance.transform.position.y,
                    startPos.position.z
                )
                + Quaternion.Euler(0,45,0) * new Vector3(
                    0,
                    0,
                    -controller.wandToPlayer.z * 2
                );
                

                charge += Time.deltaTime;
                transform.localScale = Vector3.one * charge / chargeUpTime;

                travelDir = Quaternion.Euler(0,45,0) * -new Vector3(controller.playerLook.x, 0, controller.playerLook.y);
                travelDir = travelDir.normalized;

                Debug.DrawRay(transform.position, travelDir, Color.magenta);
            }
            else if (fired && !onFired)
            {
                onFired = true;
                OnFireWand();
            }
            else
            {
                fired = true;
                _rigidBody.velocity = new Vector3(travelDir.x, transform.position.y, travelDir.z) * travelSpeed;
                Debug.DrawRay(transform.position, travelDir, Color.yellow);
            }

            yield return null;
        }

        Destroy(gameObject);
        yield return null;
    }

    // We do an extra check for enemys when firing since, if the bubble is created inside an enemy, it wont trigger the OnTriggerEnter and thus wont deal damage
    void OnFireWand()
    {
       // Debug.Log("Fired bubble awnd");
        Collider[] hits = Physics.OverlapSphere(transform.position, transform.lossyScale.magnitude, 1 << LayerMask.NameToLayer("Enemy"));
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy");
                hit.gameObject.GetComponent<EnemyHealth>().TakeDamage(Mathf.Pow(charge / chargeUpTime, 2f) * 50f);

                charge -= 0.5f;
                transform.localScale = Vector3.one * charge / chargeUpTime;
                travelSpeed *= 0.8f;

                if (charge < 0.1f) Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && fired)
        {
            if (hitEnemies.Contains(collision.gameObject)) return;
            else hitEnemies.Add(collision.gameObject);
            Debug.Log("Hit enemy");
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(Mathf.Pow(charge / chargeUpTime, 2f) * 50f);

            charge -= 0.5f;
            transform.localScale = Vector3.one * charge / chargeUpTime;
            travelSpeed *= 0.8f;

            if (charge < 0.1f) Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}