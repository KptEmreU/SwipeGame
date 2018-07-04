using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugForTouchDistance : MonoBehaviour {

    Text text;

    private void OnEnable()
    {
        EEvents.OnXChange += UpdateUI;
    }

    private void OnDisable()
    {
        EEvents.OnXChange -= UpdateUI;
    }

    private void Start()
    {
        text = GetComponent<Text>();
    }

    void UpdateUI(float x)
    {
        text.text = x.ToString();
    }


}
