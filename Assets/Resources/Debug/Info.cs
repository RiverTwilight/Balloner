using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public Text FPSinfo;

    private float deltaTime;

    private void Start()
    {
        deltaTime = 0f;

        if (PlayerPrefs.GetInt("Developer") != 1) {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Calculate the FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        // Update the FPS display
        FPSinfo.text = $"FPS: {fps:0.}";

        // Optionally, change the color of the text based on the FPS
        if (fps < 30)
        {
            FPSinfo.color = Color.yellow;
        }
        else if (fps < 15)
        {
            FPSinfo.color = Color.red;
        }
        else
        {
            FPSinfo.color = Color.green;
        }
    }
}