using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float playerSpeed = 7f;
    [SerializeField] private float jumpSpeed = 7f;
    [SerializeField] private float rotateSpeed = 7f;
    [SerializeField] private LayerMask layerMask;

    private GameInput gameInput;
    private Vector2 lastMoveDirection;
    private Vector3 playerVerticalSpeed;
    private bool isGrounded = false;
    private float groundRayDistance = 0.1f;
    private float gravityMultiplier = 3f;
    private bool jumpPressed = false;
    private Vector3 checkPointPos;
    private int fallLimit = -20;

    private void Start() {
        gameInput = GameInput.Instance;
        gameInput.OnJumpAction += GameInput_OnJumpAction;
        checkPointPos = GameObject.Find("Spawn").transform.position;
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e) {
        if (isGrounded) {
            jumpPressed = true;
        }
    }

    private void Update() {
        CheckDeath();
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement() {
        Vector2 moveDirection = lastMoveDirection;
        if (isGrounded) {
            moveDirection = gameInput.GetNormalizedMovement() * playerSpeed; // Forward/Back/Left/Right
            lastMoveDirection = moveDirection;
        }
        Vector3 playerMovement = new Vector3(moveDirection.x, 0, moveDirection.y);
        controller.Move(playerMovement * Time.deltaTime);
        transform.forward = Vector3.Slerp(transform.forward, playerMovement, Time.deltaTime * rotateSpeed);
    }

    private void HandleJump() {
        Ray ray = new Ray(transform.position + (Vector3.up * groundRayDistance), Vector3.down);
        isGrounded = jumpPressed ? false : Physics.Raycast(ray, groundRayDistance, layerMask);

        if (isGrounded) playerVerticalSpeed.y = 0;
        if (jumpPressed) {
            playerVerticalSpeed.y += Mathf.Sqrt(jumpSpeed * -Physics.gravity.y);
            jumpPressed = false;
        }
        playerVerticalSpeed.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime; // Gravity
        controller.Move(playerVerticalSpeed * Time.deltaTime);
    }

    public void HandleImpulse(Vector2 direction, float strength) {
        if (!isGrounded) {
            if (direction == Vector2.zero) {
                if (lastMoveDirection == Vector2.zero) {
                    Debug.Log("No Direction or Last Move Direction");
                    direction.x = transform.forward.x;
                    direction.y = transform.forward.z;
                } else {
                    Debug.Log("No Direction");
                    direction = lastMoveDirection.normalized;
                }
            }
            direction *= -1f;
            lastMoveDirection = direction * strength;
        }
    }

    private void CheckDeath() {
        if (controller.transform.position.y <= fallLimit) {
            controller.enabled = false;
            controller.transform.position = checkPointPos;
            controller.enabled = true;
            transform.Rotate(0, 0, 0);
        }
    }
}