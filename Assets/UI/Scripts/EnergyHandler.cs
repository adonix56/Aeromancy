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

	private void Start() {
		slider.value = 100.0f;
        //percentage.text = slider.value.ToString()+"% Breath";
	}

    public void SetMaxValue(float maxValue)
    {
        slider.maxValue = maxValue;
    }

	public void UpdateSlider(float newVal) {
		slider.value = newVal;
        if (newVal <= 0)
            NoEnergyAnim();
		//percentage.text = slider.value.ToString()+"% Breath";
	}

    private void NoEnergyAnim() {
        //cool animation here
    }
}