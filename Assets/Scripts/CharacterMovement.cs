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
    private bool canSprint = true;
    private RaycastHit lastHit;
    private float groundRayDistance = 0.1f;
    private float gravityMultiplier = 3f;
    private bool jumpPressed = false;
    private float currentPlayerSpeed;
    private float animatorSmooth = 0;
    private bool canTurn = true;

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

            if (canSprint && Input.GetKey("left shift")) {
                currentPlayerSpeed = sprintSpeed;
            } else if (canSprint) {
                currentPlayerSpeed = walkSpeed;
            }
        }
    }

    private void HandleAnimation() {
        float floatTarget;
        if (canSprint) {
            floatTarget = lastMoveDirection.magnitude / walkSpeed;
        } else {
            floatTarget = lastMoveDirection.magnitude / currentPlayerSpeed;
        }
        animatorSmooth = Mathf.SmoothStep(animatorSmooth, floatTarget, Time.deltaTime * 20f);
        characterAnimation.Move(animatorSmooth);
    }

    private void HandleMovement() {
        Vector2 moveDirection = lastMoveDirection;
        if ((gameInput.GetNormalizedMovement() * currentPlayerSpeed).x == 0.0f && (gameInput.GetNormalizedMovement() * currentPlayerSpeed).y == 0.0f){
            moveDirection = gameInput.GetNormalizedMovement() * currentPlayerSpeed; // Forward/Back/Left/Right
            lastMoveDirection = moveDirection;
        } else if (isGrounded) {
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
        if (canTurn && playerMovement != Vector3.zero)
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

    public void SetPlayerSpeed(float speed, bool lockSprint) {
        canSprint = !lockSprint;
        currentPlayerSpeed = speed;
    }

    public void SetTurning(bool turn) {
        canTurn = turn;
    }

    public void ResetPlayerSpeed() {
        currentPlayerSpeed = walkSpeed;
        canSprint = true;
    }
}