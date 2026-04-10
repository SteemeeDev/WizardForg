using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class BubbleProjectile : Projectile
{
    public float chargeUpTime = 2f;
    public float charge = 0f;

    bool fired = false;
    

    public override void Update()
    {
        base.Update();
    }

    Vector3 travelDir = Vector3.zero;
    public override IEnumerator FireProjectile(WandController controller, Transform startPos)
    {
        while (timeAlive < lifetime)
        {
            timeAlive += Time.deltaTime;


            if (Input.GetMouseButton(0) && !fired && charge <= chargeUpTime)
            {
                transform.position = new Vector3(
                    startPos.position.x,
                    PlayerController.Instance.transform.position.y,
                    startPos.position.z
                );
                charge += Time.deltaTime;
                transform.localScale = Vector3.one * charge / chargeUpTime;
                travelDir = -(PlayerController.Instance.transform.position - controller.transform.position);
                travelDir = travelDir.normalized;
                Debug.DrawRay(transform.position, travelDir, Color.magenta);
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && fired)
        {
            Debug.Log("Hit enemy");
            Destroy(collision.gameObject);

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


