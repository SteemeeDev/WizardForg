using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerWand : WandController
{
    [SerializeField] LineRenderer lazerRenderer;
    [SerializeField] Transform firePoint2;

    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonUp(0))
        {
            lazerRenderer.enabled = false;
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
        Vector3 firepos2 = firePos.position + -(transform.position - firePos.position) * 20;
        firePoint2.position = firepos2;

        Vector3 ray = firepos2 - firePos.position;
        Debug.DrawRay(firePos.position, ray - Vector3.Dot(ray, Quaternion.Euler(-45, -135, 0) * Vector3.forward) * (Quaternion.Euler(-45, -135, 0) * Vector3.forward), Color.green);

        lazerRenderer.SetPosition(1, firePoint2.position);
    }
}
