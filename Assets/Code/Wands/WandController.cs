using System.Collections;
using UnityEngine;

public class WandController : MonoBehaviour
{
    [SerializeField] public WandManager wandManager;
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator playerAnimator;
    [SerializeField] SpriteRenderer playerRenderer;
    [SerializeField] float distFromPlayer = 1f;
    public AudioSource wandAudioPlayer;
    public Transform firePos;
    public GameObject projectile;

    public float atan2;

    public float rot = 0;

    public Camera playerCam;

    public Vector3 wandToPlayer;
    public Vector3 playerLook = Vector3.zero;

    public ChargeUpBar chargeUpBar;

    public virtual void OnEnable()
    {
        if (chargeUpBar != null)
        {
            chargeUpBar.transform.parent.gameObject.SetActive(true);
        }
    }

    public virtual void OnDisable()
    {
        if (chargeUpBar != null)
        {
            chargeUpBar.transform.parent.gameObject.SetActive(false);
        }
    }

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


    public IEnumerator IEFadeAudio(float fadeTime, float targetVolume, bool stopAudio)
    {
       // Debug.Log($"FADING AUDIO TO {targetVolume}");
        float elapsed = 0;
        float startingVolume = wandAudioPlayer.volume;

        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;

            wandAudioPlayer.volume = Mathf.Lerp(startingVolume, targetVolume, elapsed / fadeTime);

            yield return null;
        }

        wandAudioPlayer.volume = targetVolume;

        if (stopAudio) wandAudioPlayer.Stop();
    }
}
