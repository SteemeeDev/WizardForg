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
                transform.position = startPos.position;
                charge += Time.deltaTime;
                transform.localScale = Vector3.one * charge / chargeUpTime;
                travelDir = transform.position - controller.transform.position;
                //travelDir = travelDir.normalized;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            Destroy(collision.gameObject);
        }
    }
}


