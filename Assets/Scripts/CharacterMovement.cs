using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 7f;
    [SerializeField] private float sprintSpeed = 12f;
    [SerializeField] private float jumpSpeed = 7f;
    [SerializeField] private float rotateSpeed = 7f;
    [SerializeField] private float slopeLimit = 30f;
    [SerializeField] private LayerMask layerMask;
    public EnergyHandler energyHandler;

    private GameInput gameInput;
    private CharacterController controller;
    private CharacterAnimation characterAnimation;
    private Vector2 lastMoveDirection;
    private Vector3 playerVerticalSpeed;
    private bool isGrounded = false;
    private RaycastHit lastHit;
    private float groundRayDistance = 0.1f;
    private float gravityMultiplier = 3f;
    private bool jumpPressed = false;
    private float currentPlayerSpeed;

    private void Start() {
        gameInput = GameInput.Instance;
        gameInput.OnJumpAction += GameInput_OnJumpAction;
        controller = CharacterManager.Instance.GetCharacterController();
        characterAnimation = CharacterManager.Instance.GetCharacterAnimation();
        currentPlayerSpeed = walkSpeed;
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e) {
        if (isGrounded && !OnSteepSlope()) {
            jumpPressed = true;
        }
    }

    private void Update() {
        if (CharacterManager.Instance.IsPlayable()) {
            HandleAnimation();
            HandleMovement();
            HandleJump();
            HandleSlope();
            UpdateGroundCheck();

            if (Input.GetKey("left shift")) {
                currentPlayerSpeed = sprintSpeed;
            } else {
                currentPlayerSpeed = walkSpeed;
            }
        }
    }

    private void HandleAnimation() {
        characterAnimation.Move(lastMoveDirection.magnitude / currentPlayerSpeed);
    }

    private void HandleMovement() {
        Vector2 moveDirection = lastMoveDirection;
        if (isGrounded) {
            moveDirection = gameInput.GetNormalizedMovement() * currentPlayerSpeed; // Forward/Back/Left/Right
            lastMoveDirection = moveDirection;
        }
        if (Mathf.Abs(moveDirection.x) > 0 || Mathf.Abs(moveDirection.y) > 0) {
            //walking
            if (currentPlayerSpeed == sprintSpeed) {
                energyHandler.UseEnergy(0.15f);
            }
        } else {
            energyHandler.GiveEnergy(0.15f);
        }
        Vector3 playerMovement = new Vector3(moveDirection.x, 0, moveDirection.y);
        controller.Move(playerMovement * Time.deltaTime);
        transform.forward = Vector3.Slerp(transform.forward, playerMovement, Time.deltaTime * rotateSpeed);
    }

    private void HandleJump() {
        if (isGrounded && playerVerticalSpeed.y < 0) playerVerticalSpeed.y = -2f;
        if (jumpPressed) {
            playerVerticalSpeed.y += Mathf.Sqrt(jumpSpeed * -Physics.gravity.y);
            jumpPressed = false;
            energyHandler.UseEnergy(7f);
        }
        playerVerticalSpeed.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime; // Gravity
        controller.Move(playerVerticalSpeed * Time.deltaTime);
    }

    public void HandleSlope()
    {
        if (OnSteepSlope())
        {
            Vector3 slopeDirection = Vector3.up - lastHit.normal * Vector3.Dot(Vector3.up, lastHit.normal);
            controller.Move(slopeDirection * (-walkSpeed) * Time.deltaTime);
        }
    }

    public void HandleImpulse(Vector2 direction, float strength) {
        if (!isGrounded) {
            if (direction == Vector2.zero) {
                if (lastMoveDirection == Vector2.zero) {
                    direction.x = transform.forward.x;
                    direction.y = transform.forward.z;
                } else {
                    direction = lastMoveDirection.normalized;
                }
            }
            transform.forward = new Vector3(direction.x, 0, direction.y).normalized;
            direction *= -1f;
            lastMoveDirection = direction * strength;
        }
    }

    public void UpdateGroundCheck()
    {
        float groundcheckRadius = 0.5f;
        bool raycast = Physics.SphereCast(
            transform.position + Vector3.up * (groundcheckRadius + groundRayDistance),
            groundcheckRadius, Vector3.down, out lastHit, groundRayDistance + 0.01f, layerMask);
        isGrounded = jumpPressed ? false : raycast;
    }

    private bool OnSteepSlope()
    {
        if (!isGrounded) return false;

        float slopeAngle = Vector3.Angle(lastHit.normal, Vector3.up);
        if (slopeAngle > slopeLimit) return true;
        return false;
    }

    public void SetPlayerSpeed(float speed) {
        currentPlayerSpeed = speed;
    }

    public void ResetPlayerSpeed() {
        currentPlayerSpeed = walkSpeed;
    }
}