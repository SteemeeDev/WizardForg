using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.LowLevel;

public class ProjectileSlingingEnemy : EnemyScript
{
    Camera mainCamera;
    [SerializeField] GameObject projectile;
    bool inPosition = false;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public override IEnumerator EnemyPathFinding()
    {
        while (true)
        {
            if (playerPosition == null)
            {
                playerPosition = PlayerController.Instance.transform;
                yield return null;
                continue;
            }
            if (inPosition)
            {
                Debug.Log("THROWING ROCK");

                GameObject proj = Instantiate(projectile);
                proj.transform.position = transform.position;
                Rigidbody rb = proj.GetComponent<Rigidbody>();
                rb.velocity = (playerPosition.position - transform.position).normalized * 10f;

                Vector3 enemyLook = mainCamera.WorldToScreenPoint(playerPosition.position) - mainCamera.WorldToScreenPoint(transform.position);
                enemyLook = enemyLook.normalized;

                float atan2 = Mathf.Atan2(enemyLook.y, enemyLook.x);
                proj.transform.rotation = Quaternion.Euler(45, 45, (180f / Mathf.PI) * atan2 + 100f);

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
        if (playerPosition != null)
        {
            if ((transform.position - playerPosition.position).magnitude < 5f)
            {
                inPosition = true;
            }
        }
    }
}
