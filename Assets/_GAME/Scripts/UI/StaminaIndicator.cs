using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SharkRush
{
    public class StaminaIndicator : MonoBehaviour
    {
        public Slider staminaUI;
        // Start is called before the first frame update
        void Start()
        {
            staminaUI = staminaUI == null ? GetComponent<Slider>() : staminaUI;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateUI(float newStamina, float min, float max)
        {
            if (staminaUI)
            {
                staminaUI.value = newStamina;
                staminaUI.minValue = min;
                staminaUI.maxValue = max;
            }
        }
    } 
}
