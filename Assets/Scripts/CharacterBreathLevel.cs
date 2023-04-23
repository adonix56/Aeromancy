using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBreathLevel : MonoBehaviour
{
    [SerializeField] private float maxBreathLevel = 100;
    //[SerializeField] private float holdBreathUseRate = 0.1f;
    [SerializeField] private EnergyHandler energyBarController;
    [SerializeField] private float restRegainBreathRate = 0.1f;

    private GameInput gameInput;
    private float currentBreathLevel;
    private CharacterMovement characterMovement;
    private bool canRegen;

    // Start is called before the first frame update
    void Start()
    {
        gameInput = GameInput.Instance;
        currentBreathLevel = maxBreathLevel;
        characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if (!characterMovement.IsMoving() && canRegen) {
            GiveEnergy(restRegainBreathRate);
        }
        //if (gameInput.IsHoldBreathPressed())
        //{
        //    UseEnergy(holdBreathUseRate);
        //}
    }

    public bool HasEnoughEnergy(float amt)
    {
        return currentBreathLevel > amt;
    }

    public void LockRegen(bool lockRegen) {
        canRegen = !lockRegen;
    }

    public void UseEnergy(float amt)
    {
        Debug.Log($"Using {amt}");
        currentBreathLevel -= amt;
        if(currentBreathLevel < 0)
        {
            currentBreathLevel = 0;
        }
        energyBarController.UpdateSlider(currentBreathLevel);


        //if (realAmt - amt >= 0.0f)
        //{
        //    realAmt -= amt;
        //    pendingRemoved += amt;
        //    float qeueuedForRemove = (int)Mathf.Floor(pendingRemoved);
        //    if (qeueuedForRemove > 0)
        //    {
        //        pendingRemoved -= qeueuedForRemove;
        //        if (realAmt - qeueuedForRemove < 0)
        //        {
        //            qeueuedForRemove = realAmt;
        //        }
        //        UpdateSlider(slider.value - qeueuedForRemove);
        //    }
        //    return true;
        //}
        //else
        //{
        //    NoEnergyAnim();
        //    return false;
        //}
    }

    public void GiveEnergy(float amt) {
        Debug.Log($"Giving {amt}");
        currentBreathLevel += amt;
        if (currentBreathLevel > maxBreathLevel)
        {
            currentBreathLevel = maxBreathLevel;
        }
        energyBarController.UpdateSlider(currentBreathLevel);

        // if (slider.value + amt <= 100.0f){
        //pendingAdded += amt;
        //int qeueuedForAddition = (int)Mathf.Floor(pendingAdded);
        //if (qeueuedForAddition > 0)
        //{
        //    pendingAdded -= qeueuedForAddition;
        //    UpdateSlider(slider.value + qeueuedForAddition);
        //}
        // } else {
        //     UpdateSlider(100.0f);
        // }
    }

    public void RestoreEnergy()
    {
        currentBreathLevel = maxBreathLevel;
        energyBarController.UpdateSlider(currentBreathLevel);
    }

    public bool IsHoldingBreath()
    {
        return gameInput.IsHoldBreathPressed();
    }
}
