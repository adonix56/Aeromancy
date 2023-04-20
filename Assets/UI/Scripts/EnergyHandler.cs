using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnergyHandler : MonoBehaviour
{
	public Slider slider;
	//public TMP_Text percentage;
    
    private float pendingAdded = 0;
    private float pendingRemoved = 0;
    private float realAmt = 100.0f;

	private void Start() {
		slider.value = 100.0f;
        //percentage.text = slider.value.ToString()+"% Breath";
	}

    public bool UseEnergy(float amt) {
        if (realAmt - amt >= 0.0f){
            realAmt -= amt;
            pendingRemoved += amt;
            float qeueuedForRemove = (int)Mathf.Floor(pendingRemoved);
            if (qeueuedForRemove > 0) {
                pendingRemoved -= qeueuedForRemove;
                if (realAmt - qeueuedForRemove < 0) {
                    qeueuedForRemove = realAmt;
                }
                UpdateSlider(slider.value - qeueuedForRemove);
            }
            return true;
        } else {
            NoEnergyAnim();
            return false;
        }
    }

    public void GiveEnergy(float amt) {
        // if (slider.value + amt <= 100.0f){
            pendingAdded += amt;
            int qeueuedForAddition = (int)Mathf.Floor(pendingAdded);
            if (qeueuedForAddition > 0) {
                pendingAdded -= qeueuedForAddition;
                UpdateSlider(slider.value + qeueuedForAddition);
            }
        // } else {
        //     UpdateSlider(100.0f);
        // }
    }

	private void UpdateSlider(float newVal) {
		slider.value = newVal;
        realAmt = slider.value;
		//percentage.text = slider.value.ToString()+"% Breath";
	}

    private void NoEnergyAnim() {
        //cool animation here
    }
}