using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    void Start()
    {
        Rect safeArea = Screen.safeArea;
        float notchHeight = Screen.height - safeArea.height;
        float notchY = Screen.height - notchHeight;

        if (notchHeight > 0)
        {
            //RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            //rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y - notchHeight, rectTransform.position.z);
            Debug.Log("The notch is located at the top of the screen");
        }

        // The notch is located at the bottom of the screen
        if (notchY == safeArea.yMax)
        {
            // Do something
        }

        // The notch is located on the left or right side of the screen
        if (notchY > 0 && notchY < Screen.height)
        {
            // Do something
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
