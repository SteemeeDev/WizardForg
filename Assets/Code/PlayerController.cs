using UnityEngine;

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
            DontDestroyOnLoad(gameObject);
        }
    }


    [SerializeField] CharacterController controller;
    [SerializeField] float moveSpeed = 4f;

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = (Quaternion.Euler(0, 45, 0) * new Vector3(inputX, 0, inputY));
        moveDir = Vector3.Normalize(moveDir);

        controller.Move(moveDir * Time.deltaTime * moveSpeed);
    }
}
