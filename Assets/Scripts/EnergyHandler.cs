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

	private void Start() {
		slider.value = 100.0f;
        percentage.text = slider.value.ToString()+"% Breath";
	}

    public void UseEnergy(float amt) {
        if (slider.value - amt >= 0.0f){
            UpdateSlider(slider.value - amt);
        } else {
            
        }
    }

	private void UpdateSlider(float newVal) {
		slider.value = newVal;
		percentage.text = slider.value.ToString()+"% Breath";
	}

    private void NoEnergyAnim() {
        //cool animation here
    }
}