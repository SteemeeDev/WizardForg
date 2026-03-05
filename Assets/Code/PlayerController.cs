using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
