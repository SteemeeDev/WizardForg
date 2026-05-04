using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleWand : WandController
{
    [SerializeField] ChargeUpBar chargeUpBar;

    private void OnEnable()
    {
        if (chargeUpBar != null)
        {
            chargeUpBar.transform.parent.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (chargeUpBar != null)
        {
            chargeUpBar.transform.parent.gameObject.SetActive(false);
        }
    }

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
    