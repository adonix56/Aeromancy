using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnJumpAction;
    public event EventHandler OnSkill1Action;
    public event EventHandler OnSkill2Action;

    private InputSystemActions inputSystemActions;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null) {
            Debug.LogError("More than one GameInput Object found.");
        }
        Instance = this;

        inputSystemActions = new InputSystemActions();
        inputSystemActions.Player.Enable();

        inputSystemActions.Player.Jump.performed += Jump_Performed;
        inputSystemActions.Player.Skill_1.performed += Skill_1_Performed;
        inputSystemActions.Player.Skill_2.performed += Skill_2_Performed;
    }

    private void Jump_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    private void Skill_1_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnSkill1Action?.Invoke(this, EventArgs.Empty);
    }

    private void Skill_2_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnSkill2Action?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetNormalizedMovement() {
        Vector2 raw = inputSystemActions.Player.Movement.ReadValue<Vector2>();
        Vector2 forward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
        Vector2 right = new Vector2(Camera.main.transform.right.x, Camera.main.transform.right.z);

        forward.Normalize();
        right.Normalize();

        return forward * raw.y + right * raw.x;
    }
}
