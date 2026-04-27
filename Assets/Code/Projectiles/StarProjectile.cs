using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class StarProjectile : Projectile
{
    Vector3 startingPos;
    Vector3 targetPos;
    LayerMask groundLayer;
    private void Awake()
    {
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
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

        Destroy(gameObject);
    }
}
