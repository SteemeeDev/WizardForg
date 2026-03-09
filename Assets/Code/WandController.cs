using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class WandController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    public float rot = 0;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    Vector3 wandToPlayer;

    private void Update()
    {
        Vector3 playerScreenPos = cam.WorldToScreenPoint(playerTransform.position);
        Vector3 mousePos = Input.mousePosition;

        Vector3 playerLook = playerScreenPos - mousePos;
        playerLook = playerLook.normalized;

        wandToPlayer = playerTransform.position - Quaternion.Euler(0, 45, 0) * new Vector3(playerLook.x, 0, playerLook.y);
        transform.position = wandToPlayer;

        float atan2 = Mathf.Atan2(playerLook.y, playerLook.x);
        transform.rotation = Quaternion.Euler(45, 45, (180f / Mathf.PI) * atan2 + 90f);
    }
}
