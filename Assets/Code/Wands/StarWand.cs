using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class StarWand : WandController
{
    [SerializeField] Transform firePos2;
    public override void FireWand()
    {
        GameObject proj = Instantiate(projectile);
        Projectile projManager = proj.GetComponent<Projectile>();

        proj.transform.position = Vector3.Lerp(firePos.position, firePos2.position, Random.Range(0f, 1f));

        projManager.StartCoroutine(projManager.FireProjectile(this, proj.transform));
    }
}
