using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBreathLevel : MonoBehaviour
{
    [SerializeField] private float maxBreathLevel = 100;
    [SerializeField] private float holdBreathUseRate = 0.1f;
    [SerializeField] private EnergyHandler energyBarController;

    private GameInput gameInput;
    private float currentBreathLevel;

    // Start is called before the first frame update
    void Start()
    {
        gameInput = GameInput.Instance;
        currentBreathLevel = maxBreathLevel;
    }

    private void Update()
    {
        if (gameInput.IsHoldBreathPressed())
        {
            UseEnergy(holdBreathUseRate);
        }
    }

    public bool HasEnergy()
    {
        return currentBreathLevel > 0;
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

    public void GiveEnergy(float amt)
    {
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
