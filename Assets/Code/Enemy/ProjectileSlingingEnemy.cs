using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.LowLevel;

public class ProjectileSlingingEnemy : EnemyScript
{
    Camera mainCamera;
    [SerializeField] GameObject projectile;
    AudioSource _audioSource;
    bool inPosition = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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
                GameObject proj = Instantiate(projectile);
                proj.transform.position = transform.position;

                _audioSource.Play();

                Rigidbody rb = proj.GetComponent<Rigidbody>();
                rb.velocity = (
                    (playerPosition.position 
                    + PlayerController.Instance.moveDir
                    * PlayerController.Instance.moveSpeed
                    * Vector3.Distance(playerPosition.position, transform.position) * 0.1f
                    + new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f)) * PlayerController.Instance.moveDir.magnitude) // Dont use any randomness if player isnt moving
                    - transform.position).normalized * 10f
                ;

                Vector3 enemyLook =
                    mainCamera.WorldToScreenPoint(proj.transform.position + rb.velocity)
                    - mainCamera.WorldToScreenPoint(transform.position);
                enemyLook = enemyLook.normalized;

                float atan2 = Mathf.Atan2(enemyLook.y, enemyLook.x);
                proj.transform.rotation = Quaternion.Euler(45, 45, (180f / Mathf.PI) * atan2 + 90f);

                Agent.SetDestination(transform.position + new Vector3(Random.Range(-3f, -3f), 0, Random.Range(-3f,3f)));

                yield return new WaitForSeconds(Random.Range(0.75f, 1.25f));

                inPosition = false;
                Agent.SetDestination(playerPosition.position);
            }
            else
            {
                Agent.SetDestination(playerPosition.position);
            }

            yield return new WaitForSeconds(Random.Range(2f,4f));
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
