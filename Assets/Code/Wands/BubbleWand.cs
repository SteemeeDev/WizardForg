using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleWand : WandController
{
    public AudioSource bubblePop;
    public override void FireWand()
    {
        GameObject proj = Instantiate(projectile);
        BubbleProjectile projManager = proj.GetComponent<BubbleProjectile>();

        Vector3 fireDir = (transform.position - firePos.position);

        projManager.StartCoroutine(
            projManager.FireBubble(this, firePos, chargeUpBar)
        );
    }
}
    