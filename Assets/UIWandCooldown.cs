using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWandCooldown : MonoBehaviour
{
    public float cooldown;
    public float maxCooldown;
    RectTransform _rectTransform;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        _rectTransform.sizeDelta = new Vector2(80, cooldown / maxCooldown * 80);
    }
}
