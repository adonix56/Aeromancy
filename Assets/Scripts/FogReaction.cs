using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogReaction : MonoBehaviour
{
    public float timeForDamage = 2.0f;

    private GameInput gameInput;
    private int currentFogAmount; // the number of fog blocks in which the character is currently inside
    private float timeForDamageTimer;
    private CharacterHealth characterHealth;

    void Start()
    {
        currentFogAmount = 0;
        timeForDamageTimer = 0;
        gameInput = GameInput.Instance;
        characterHealth = GetComponent<CharacterHealth>();
    }

    private void Update()
    {
        UpdateTimeInsideFog();
    }

    private void UpdateTimeInsideFog()
    {
        if (currentFogAmount > 0)
        {
            if (gameInput.IsHoldBreathPressed())
            {
                timeForDamageTimer -= Time.deltaTime;
                timeForDamageTimer = Mathf.Max(timeForDamageTimer, 0);
            }
            else
            {
                timeForDamageTimer += Time.deltaTime;
                if (timeForDamageTimer > timeForDamage)
                {
                    characterHealth.GetHit();
                    timeForDamageTimer = 0;
                }
            }
        }
    }

    public void EnterFogBlock(FogBlock enteredFogBlock)
    {
        currentFogAmount++;

        EnvironmentManager envManager = GameObject.Find("GameManager").GetComponent<EnvironmentManager>();
        if (envManager)
        {
            if (enteredFogBlock.isEdge)
            {
                envManager.ChangeToDefaultEnvironment();
            }
            else
            {
                envManager.ChangeToFogEnvironment();
            }
        }
    }

    public void ExitFogBlock(FogBlock enteredFogBlock)
    {
        currentFogAmount--;
    }
}
