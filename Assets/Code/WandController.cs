using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class WandController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject projectile;
    [SerializeField] Animator playerAnimator;
    [SerializeField] SpriteRenderer playerRenderer;

    public float atan2;

    public float rot = 0;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    Vector3 wandToPlayer;
    Vector3 playerLook = Vector3.zero;

    private void Update()
    {
        Vector3 playerScreenPos = cam.WorldToScreenPoint(playerTransform.position);
        Vector3 mousePos = Input.mousePosition;

        playerLook = playerScreenPos - mousePos;
        playerLook = playerLook.normalized;

        wandToPlayer = playerTransform.position - Quaternion.Euler(0, 45, 0) * new Vector3(playerLook.x, 0, playerLook.y);
        transform.position = wandToPlayer;

        atan2 = Mathf.Atan2(playerLook.y, playerLook.x);
        transform.rotation = Quaternion.Euler(45, 45, (180f / Mathf.PI) * atan2 + 90f);

        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile();
        }

        RotatePlayer();
    }

    void RotatePlayer()
    {
        float adjustedAtan = Mathf.Atan2(playerLook.x, playerLook.y) * (180f / Mathf.PI);
       // Debug.Log(adjustedAtan);


        if (Mathf.Sign(adjustedAtan) == 1f)
        {
            playerRenderer.flipX = true;
        }
        else
        {
            playerRenderer.flipX = false;
        }

        playerAnimator.SetFloat("TurnDegrees", adjustedAtan);
    }

    void FireProjectile()
    {
        GameObject proj = Instantiate(projectile);
        proj.transform.position = transform.position;

        Projectile projectileManager = proj.GetComponent<Projectile>();

        projectileManager.travelDir = (transform.position - playerTransform.position)* 10; 
    }
}
