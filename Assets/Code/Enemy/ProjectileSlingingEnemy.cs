using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileSlingingEnemy : EnemyScript
{
    [SerializeField] GameObject projectile;
    bool inPosition = false;

    public override IEnumerator EnemyPathFinding()
    {
        while (true)
        {
            if (inPosition)
            {
                Debug.Log("THROWING ROCK");

                GameObject proj = Instantiate(projectile);
                proj.transform.position = transform.position;
                Rigidbody rb = proj.GetComponent<Rigidbody>();
                rb.velocity = (playerPosition.position - transform.position).normalized * 10f;
                rb.angularVelocity = new Vector3(0, 0, 10f);
                Agent.SetDestination(transform.position);

                yield return new WaitForSeconds(1f);

                inPosition = false;
                Agent.SetDestination(playerPosition.position);
            }
            else
            {
                Agent.SetDestination(playerPosition.position);
            }

            yield return new WaitForSeconds(3f);
        }
    }

    public override void Update()
    {
        base.Update();
        if ((transform.position - playerPosition.position).magnitude < 5f)
        {
            inPosition = true;
        }
    }
}
