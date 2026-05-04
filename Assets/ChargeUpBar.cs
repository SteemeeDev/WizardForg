using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeUpBar : MonoBehaviour
{
    [SerializeField] float _charge;
    public float maxCharge;

    [SerializeField] float targetScale;
    
    public void UpdateBar(float charge)
    {
        transform.localScale = new Vector3((charge / maxCharge) * targetScale, transform.localScale.y, transform.localScale.z);
    }
}
