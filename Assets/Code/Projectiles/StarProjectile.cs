using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class StarProjectile : Projectile
{
    [SerializeField] float onHitDamage = 80;
    Vector3 startingPos;
    Vector3 targetPos;
    LayerMask groundLayer;

    LayerMask enemyLayer;
    private void Awake()
    {
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive > lifetime)
        {
            Destroy(gameObject);
        }
    }

    public override IEnumerator FireProjectile(WandController controller, Transform startPos)
    {
        RaycastHit hit;
        if(Physics.Raycast(controller.playerCam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, groundLayer)){
            targetPos = hit.point;
        }
        float elapsed = 0;
        float animTime = 2f;

        startingPos = transform.position;

        while (elapsed < animTime)
        {
            Debug.DrawLine(startPos.position, targetPos, Color.red);
            elapsed += Time.deltaTime;

           // _rigidBody.MovePosition(Vector3.Lerp(startPos.position, targetPos, elapsed / animTime));
            transform.position = Vector3.Lerp(startingPos, targetPos, elapsed / animTime);
            _rigidBody.angularVelocity = new Vector3(0, 0, 10f);

            yield return null;
        }

        CameraShake.Instance.StartCoroutine(CameraShake.Instance.IEShakeCamera(0.3f, 0.3f));
        HitGround();

        _rigidBody.angularVelocity = Vector3.zero;
        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);
    }

    void HitGround()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4f, enemyLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<EnemyHealth>().TakeDamage(onHitDamage);
            }
        }
    }
}
