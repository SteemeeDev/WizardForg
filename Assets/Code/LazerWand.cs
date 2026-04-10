using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;
using static UnityEngine.UI.Image;

public class LazerWand : WandController
{
    [SerializeField] LineRenderer lazerRenderer;
    [SerializeField] float maxLazerLength = 20f;
    [SerializeField] Transform firePoint2;

    int layerMask;

    private void Start()
    { 
        layerMask = (1 << LayerMask.NameToLayer("3dEnvironment")) | (1 << LayerMask.NameToLayer("Enemy"));
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonUp(0))
        {
            lazerRenderer.enabled = false;
            firePoint2.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            FireWand();
        }
    }

    public override void FireWand()
    {
        lazerRenderer.enabled = true;
        lazerRenderer.SetPosition(0, firePos.position);


        if (Physics.BoxCast(
            center: firePos.position - firePos.up * 2,
            halfExtents: new Vector3(0.1f, 0.01f, 20f),
            direction: firePos.up,
            out RaycastHit hit,
            orientation: firePos.rotation,
            maxDistance: maxLazerLength,
            layerMask: layerMask
        ))
        {
            if (hit.distance < 2f) lazerRenderer.enabled = false; 
            else lazerRenderer.enabled = true;

            Vector3 firepos2 = firePos.position + (firePos.up * hit.distance) - firePos.up * 2;
            firePoint2.position = firepos2;
            firePoint2.gameObject.SetActive(true);
            lazerRenderer.SetPosition(1, firepos2);

             if (hit.transform.gameObject.CompareTag("Enemy"))
             {
                 Destroy(hit.transform.gameObject);
             }
        }
        else
        {
            Vector3 firepos2 = firePos.position + (firePos.up * maxLazerLength);
            firePoint2.position = firepos2;
            lazerRenderer.SetPosition(1, firepos2);
        }

    }
}
