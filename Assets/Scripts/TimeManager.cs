using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class TimeManager : MonoBehaviour
{
    public bool isNight = false;
    public bool switchLocked = false;
    public int switchInterval = 100;

    public GameObject NightBackground;
    public GameObject DayBackground;
    public GameObject StarSky;

    public GameObject Star_Prefab;

    // Start is called before the first frame update
    //void Start()
    //{
    //    NightBackground.GetComponent<Image>().CrossFadeAlpha(0, 0f, false);
    //}

    //// Update is called once per frame
    void Update()
    {
        SwitchTime();
    }

    public async void SwitchNight()
    {
        CanvasGroup canvaGroup = NightBackground.GetComponent<CanvasGroup>();
        while (canvaGroup.alpha < 1f)
        {
            canvaGroup.alpha += 0.05f;
            StarSky.GetComponent<CanvasGroup>().alpha += 0.05f;
            await UniTask.Delay(100);
        }
    }

    public async void SwitchDay()
    {
        CanvasGroup canvaGroup = NightBackground.GetComponent<CanvasGroup>();
        while (canvaGroup.alpha > 0f)
        {
            canvaGroup.alpha -= 0.05f;
            StarSky.GetComponent<CanvasGroup>().alpha -= 0.05f;

            await UniTask.Delay(100);
        }
        foreach (Transform child in StarSky.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void GenerateStarsky()
    {
        int i = 0;
        while (i < 7)
        {
            Instantiate(Star_Prefab, StarSky.transform);
            i++;
        }

    }

    public void SwitchTime()
    {
        int currentHeight = gameObject.GetComponent<GameManager>().currentHeight;

        if (currentHeight > switchInterval && currentHeight % switchInterval > switchInterval / 2)
        {
            switchLocked = false;
        }

        if (!switchLocked && currentHeight > switchInterval && currentHeight % switchInterval <= 10)
        {
            isNight = !isNight;
            if (isNight)
            {
                SwitchNight();
                GenerateStarsky();
            }
            else
            {
                SwitchDay();
            }
            switchLocked = true;
        }
    }
}
