using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnJumpAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnHoldBreathAction;
    // Left Mouse Button
    public event EventHandler OnSkill0Action;
    // Right Mouse Button
    public event EventHandler OnSkill1Action;
    // Spacebar
    public event EventHandler OnSkill2Action;
    // Shift Key
    public event EventHandler OnSkill3Action;
    // F Key
    public event EventHandler OnSkill4Action;

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
        inputSystemActions.Player.Jump.Disable(); // FOR THE MOMENT WE DONT WANT JUMPING BEHAVIOUR

        //For Non-Holding Skills
        inputSystemActions.Player.Skill_0.performed += Skill_0_Performed;
        inputSystemActions.Player.Skill_1.performed += Skill_1_Performed;
        inputSystemActions.Player.Skill_2.performed += Skill_2_Performed;
        inputSystemActions.Player.Skill_3.performed += Skill_3_Performed;
        inputSystemActions.Player.Skill_4.performed += Skill_4_Performed;
        inputSystemActions.Player.Pause.performed += Pause_Performed;
    }

    private void Jump_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    //private void Hold_Breath_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    //{
    //    OnHoldBreathAction?.Invoke(this, EventArgs.Empty);
    //}

    //For Non-Holding Skills
    private void Skill_0_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnSkill0Action?.Invoke(this, EventArgs.Empty);
    }

    private void Skill_1_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnSkill1Action?.Invoke(this, EventArgs.Empty);
    }

    private void Skill_2_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnSkill2Action?.Invoke(this, EventArgs.Empty);
    }

    private void Skill_3_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnSkill3Action?.Invoke(this, EventArgs.Empty);
    }

    private void Skill_4_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        OnSkill4Action?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetNormalizedMovement() {
        Vector2 raw = inputSystemActions.Player.Movement.ReadValue<Vector2>();
        Vector2 forward = new Vector2(Camera.main.transform.forward.x, Camera.main.transform.forward.z);
        Vector2 right = new Vector2(Camera.main.transform.right.x, Camera.main.transform.right.z);

        forward.Normalize();
        right.Normalize();

        return forward * raw.y + right * raw.x;
    }

    public Vector2 GetAxis()
    {
        return inputSystemActions.Player.Look.ReadValue<Vector2>();
    }

    public Vector2 GetRawMovement()
    {
        Vector2 rawMovement = inputSystemActions.Player.Movement.ReadValue<Vector2>();
        rawMovement.y = Mathf.Max(rawMovement.y, 0); // Forbid going backwards
        return rawMovement;
    }

    //For Holding Skills
    public bool isSkillPressed(int index)
    { // 0 LMB - 1 RMB - 2 SPACE - 3 SHIFT - 4 F
        if (index == 0)
            return inputSystemActions.Player.Skill_0.ReadValue<float>() == 1f;
        if (index == 1)
            return inputSystemActions.Player.Skill_1.ReadValue<float>() == 1f;
        if (index == 2)
            return inputSystemActions.Player.Skill_2.ReadValue<float>() == 1f;
        if (index == 3)
            return inputSystemActions.Player.Skill_3.ReadValue<float>() == 1f;
        return inputSystemActions.Player.Skill_4.ReadValue<float>() == 1f;
    }
    public bool IsHoldBreathPressed()
    {
        return inputSystemActions.Player.Skill_2.ReadValue<float>() == 1f;
    }
}
