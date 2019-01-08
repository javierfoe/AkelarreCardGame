﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlayerAmountSlider : MonoBehaviour {

    private void Start()
    {
        Slider slider = GetComponent<Slider>();
        slider.onValueChanged.Invoke(slider.value);
    }
}