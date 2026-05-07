using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;
    public static PlayerController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
           // DontDestroyOnLoad(gameObject);
        }
    }


    [SerializeField] Rigidbody rigidBody;
    [SerializeField] GameObject smokeEffect;
    [SerializeField] SpriteRenderer playerSprite;
    
    public float moveSpeed = 4f;
    [SerializeField] float dashCooldown = 1f;
    float timeSinceLastDash = 0;
    public WandManager wandManager;

    public Vector3 moveDir;

    AudioSource dashAudioSource;
    private void Start()
    {
        dashAudioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(inputX, 0, inputY);
        moveDir = Quaternion.Euler(0, 45, 0) * moveDir;
        moveDir = Vector3.Normalize(moveDir);

        rigidBody.MovePosition(transform.position + moveDir * Time.fixedDeltaTime * moveSpeed);
    }

    private void Update()
    {
        timeSinceLastDash += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && timeSinceLastDash > dashCooldown)
        {
            dashAudioSource.Play();
            timeSinceLastDash = 0;
            GameObject smoke = Instantiate(smokeEffect, transform.position, transform.rotation);
            smoke.GetComponent<SmokeParticle>().spriteRenderer.flipX = playerSprite.flipX;
            rigidBody.MovePosition(transform.position + moveDir.normalized * moveSpeed * 0.5f);
        }
    }

}
