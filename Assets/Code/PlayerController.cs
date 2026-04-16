using UnityEngine;
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

    [SerializeField] WandController[] wands;
    [SerializeField] int currentWandIndex;
    public WandController currentWand;

    [SerializeField] CharacterController controller;
    [SerializeField] float moveSpeed = 4f;

    private void Start()
    {
        SwitchWand();
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(inputX, 0, inputY);
        moveDir = Vector3.Normalize(moveDir);
        moveDir += new Vector3(0, 0, inputY * 0.6f); // Y movement feels slower for some reason?
        moveDir = Quaternion.Euler(0, 45, 0) * moveDir;

        controller.Move(moveDir * Time.deltaTime * moveSpeed);

        // Yandere dev ahh code :sob:
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWandIndex = 0;
            SwitchWand();
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWandIndex = 1;
            SwitchWand();
        }

       
    }

    void SwitchWand()
    {
        if (currentWand != null) currentWand.gameObject.SetActive(false);
        currentWand = wands[currentWandIndex];
        currentWand.gameObject.SetActive(true);
    }
}
