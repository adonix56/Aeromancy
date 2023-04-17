using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnergyHandler : MonoBehaviour
{
	public Slider slider;
	public TMP_Text percentage;
    public GameObject bg;
    
    private float pendingAdded = 0;

	private void Start() {
		slider.value = 100.0f;
        percentage.text = slider.value.ToString()+"% Breath";
	}

    public bool UseEnergy(float amt) {
        if (slider.value - amt >= 0.0f){
            UpdateSlider(slider.value - amt);
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
		percentage.text = slider.value.ToString()+"% Breath";
	}

    private void NoEnergyAnim() {
        //cool animation here
    }
}