using UnityEngine;
using UnityEngine.EventSystems;

public class SecretCodeCheck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float pointerDownTimer = 0;
    private float allowedLongPressTime = 1f; // 1 second for long press
    private int shortPressCount = 0;
    private bool longPressDetected = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownTimer = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (longPressDetected && Time.time - pointerDownTimer < allowedLongPressTime)
        {
            shortPressCount++;

            if (shortPressCount == 2)
            {
                int currentPrefs = PlayerPrefs.GetInt("Developer");

                if (currentPrefs != 1)
                {
                    PlayerPrefs.SetInt("Developer", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("Developer", 0);
                }

                PlayerPrefs.Save();

                longPressDetected = false;
                shortPressCount = 0;

                Debug.Log(":123123");
            }
        }
        else if (Time.time - pointerDownTimer >= allowedLongPressTime)
        {
            longPressDetected = true;
            shortPressCount = 0;
        }
    }
}
