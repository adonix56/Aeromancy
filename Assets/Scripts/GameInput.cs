using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnJumpAction;
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
    }

    private void Jump_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetNormalizedMovement() {
        return inputSystemActions.Player.Movement.ReadValue<Vector2>().normalized;
    }
}
