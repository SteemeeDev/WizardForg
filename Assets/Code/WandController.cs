using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float rot = 0;

    [SerializeField] Vector3 forward = new Vector3(-45, -135, 0);

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 playerScreenPos = cam.WorldToScreenPoint(playerTransform.position);
        Vector3 mousePos = Input.mousePosition;

        Vector3 playerLook = playerScreenPos - mousePos;

        playerLook = playerLook.normalized;

        transform.position = playerTransform.position - Quaternion.Euler(0, 45, 0) * new Vector3(playerLook.x, 0, playerLook.y);

        //transform.rotation = Quaternion.LookRotation(forward) * Quaternion.AngleAxis(rot, forward);
    }
}
