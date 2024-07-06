using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
        Debug.Log($"Updated Health Bar: {currentValue} / {maxValue} = {slider.value}");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
