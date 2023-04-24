using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBreathLevel : MonoBehaviour
{
    [SerializeField] private float maxBreathLevel = 100;
    //[SerializeField] private float holdBreathUseRate = 0.1f;
    [SerializeField] private EnergyHandler energyBarController;
    [SerializeField] private float restRegainBreathRate = 0.1f;
    [SerializeField] private float walkRegainBreathRate = 0.04f;

    private GameInput gameInput;
    private float currentBreathLevel;
    private CharacterMovement characterMovement;
    private int regenLockNumber;

    // Start is called before the first frame update
    void Start()
    {
        gameInput = GameInput.Instance;
        energyBarController.SetMaxValue(maxBreathLevel);
        currentBreathLevel = maxBreathLevel;
        characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if(CanRegen())
        {
            if(!characterMovement.IsMoving())
            {
                GiveEnergy(restRegainBreathRate);
            }
            else if (!characterMovement.IsSprinting())
            {
                GiveEnergy(walkRegainBreathRate);
            }
        }
    }

    public bool HasEnoughEnergy(float amt)
    {
        return currentBreathLevel > amt;
    }

    public bool CanRegen()
    {
        return regenLockNumber <= 0;
    }

    public void LockRegen(bool lockRegen) {
        if (lockRegen)
            regenLockNumber++;
        else
            regenLockNumber--;
    }

    public void UseEnergy(float amt)
    {
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
