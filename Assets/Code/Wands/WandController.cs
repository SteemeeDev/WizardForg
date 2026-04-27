using UnityEngine;

public class WandController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    public GameObject projectile;
    [SerializeField] Animator playerAnimator;
    [SerializeField] SpriteRenderer playerRenderer;
    public Transform firePos;
    [SerializeField] float distFromPlayer = 1f;

    public float atan2;

    public float rot = 0;

    public Camera playerCam;

    public Vector3 wandToPlayer;
    public Vector3 playerLook = Vector3.zero;

    public virtual void Update()
    {
        Vector3 playerScreenPos = playerCam.WorldToScreenPoint(playerTransform.position);
        Vector3 mousePos = Input.mousePosition;

        playerLook = playerScreenPos - mousePos;
        playerLook = playerLook.normalized;

        transform.position = playerTransform.position - Quaternion.Euler(0, 45, 0) * new Vector3(playerLook.x, 0, playerLook.y) * distFromPlayer;
        wandToPlayer = new Vector3(playerLook.x, 0, playerLook.y) * distFromPlayer;

        atan2 = Mathf.Atan2(playerLook.y, playerLook.x);
        transform.rotation = Quaternion.Euler(45, 45, (180f / Mathf.PI) * atan2 + 90f);

        if (Input.GetMouseButtonDown(0))
        {
            FireWand();
        }

        RotatePlayer();
    }

    public virtual void FireWand()
    {
        GameObject proj = Instantiate(projectile);
        Projectile projManager = proj.GetComponent<Projectile>();

        Vector3 fireDir = (transform.position - firePos.position);

        projManager.StartCoroutine(
            projManager.FireProjectile(this, firePos)
        );
    }
    void RotatePlayer()
    {
        float adjustedAtan = Mathf.Atan2(playerLook.x, playerLook.y) * (180f / Mathf.PI);


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

}
