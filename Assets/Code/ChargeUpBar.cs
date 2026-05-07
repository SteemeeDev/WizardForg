using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeUpBar : MonoBehaviour
{
    [SerializeField] float _charge;
    public float maxCharge;

    [SerializeField] float targetScale;
    [SerializeField] UIWandCooldown _UIWandCooldown;

    [SerializeField] WandManager wandManager;
    [SerializeField] WandManager.Wand wandType;
    
    public void UpdateBar(float charge)
    {
        _charge = charge;
        transform.localScale = new Vector3((charge / maxCharge) * targetScale, transform.localScale.y, transform.localScale.z);

        if (wandType == WandManager.Wand.LazerWand)
        {
            if (wandManager.wands[(int)WandManager.Wand.LazerWand].GetComponent<LazerWand>().overCharged)
            {
                _UIWandCooldown.cooldown = charge;
            }
            else
            {
                _UIWandCooldown.cooldown = 0f;
            }
        }
        if (wandType == WandManager.Wand.StarWand)
        {
            _UIWandCooldown.cooldown = maxCharge - charge;
        }
    }
}
