using Unity.VisualScripting;
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
    [SerializeField] Texture2D cursor;
    
    public float moveSpeed = 4f;
    [SerializeField] float dashCooldown = 1f;
    float timeSinceLastDash = 0;
    public WandManager wandManager;

    public Vector3 moveDir;

    AudioSource dashAudioSource;
    private void Start()
    {
        dashAudioSource = GetComponent<AudioSource>();
        Cursor.SetCursor(cursor, new Vector2(14, 14), CursorMode.Auto);
    }
    private void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(inputX, 0, inputY);
        moveDir = Quaternion.Euler(0, 45, 0) * moveDir;
        moveDir = Vector3.Normalize(moveDir);


        rigidBody.velocity = Vector3.zero;
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

            // Spherecast to check for obstacles, so we cant dash trough them
            RaycastHit hit;
            Physics.SphereCast(
                transform.position + -moveDir.normalized,
                1f,
                moveDir.normalized,
                out hit, 
                (moveDir.normalized * moveSpeed * 0.5f).magnitude,
                1 << LayerMask.NameToLayer("3dEnvironment")
            );
          

            // If we hit environment, shorten the dash length
            if (hit.collider != null)
            {
                Vector3 newPosition = hit.point - (moveDir.normalized * moveSpeed * 0.5f).normalized * .1f;
                Debug.DrawLine(transform.position + -moveDir.normalized, newPosition, Color.magenta, 5f);
                rigidBody.MovePosition(new Vector3(newPosition.x, transform.position.y, newPosition.z));
                rigidBody.velocity = Vector3.zero;
            }
            // Dash normally if we hit nothing
            else
            {
                rigidBody.MovePosition(transform.position + moveDir.normalized * moveSpeed * 0.5f);
                rigidBody.velocity = Vector3.zero;
            }
        }
    }

}
