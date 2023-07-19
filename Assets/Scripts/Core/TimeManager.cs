using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;

public class TimeManager : MonoBehaviour
{
    public enum SkyType
    {
        DaySky,
        LowDaySky,
        NightSky
    }

    [Title("Time Status")]
    public bool isNight = false;
    [ReadOnly] public bool reachCloudsea = false;
    [ReadOnly] public bool switchLocked = false;

    [Range(10, 1000)]
    public int switchInterval = 100;
    public int currentHeight;
    public int cloudseaHeight;

    public GameManager gameManager;
    public Image SkyContainer;
    public Image SkyBackContainer;
    public Image DayCloudSea;
    public Image NightCloudSea;
    public GameObject NearCloudsea;

    public GameObject StarSky;

    public GameObject Star_Prefab;

    public Sprite DaySky;
    public Sprite LowDaySky;
    public Sprite NightSky;

    private Dictionary<SkyType, Sprite> skySprites = new Dictionary<SkyType, Sprite>();
    private SkyType currentSkyType;
    [ReadOnly] public bool dayNightCycleStarted = false;
    void Awake()
    {
        gameManager = gameObject.GetComponent<GameManager>();

        currentSkyType = SkyType.LowDaySky;

        skySprites[SkyType.DaySky] = DaySky;
        skySprites[SkyType.LowDaySky] = LowDaySky;
        skySprites[SkyType.NightSky] = NightSky;
    }
    void Update()
    {
        currentHeight = gameManager.currentHeight;

        if (dayNightCycleStarted)
        {
            SwitchTime();
        }

        if (currentHeight > cloudseaHeight && !reachCloudsea)
        {
            reachCloudsea = true;
            EnableDayCloudSea();
            ChangeSky(SkyType.DaySky);
            dayNightCycleStarted = true;
        }
    }
    private void TransitionSky(SkyType targetSky)
    {
        float duration = 4f; // Set this to the duration you want for the transition

        // Copy the current sprite from the back container to the front container and set its alpha to 1
        SkyContainer.sprite = SkyBackContainer.sprite;
        Color frontColor = SkyContainer.color;
        frontColor.a = 1;
        SkyContainer.color = frontColor;

        SkyBackContainer.sprite = skySprites[targetSky];

        SkyContainer.DOFade(0, duration).OnComplete(() => switchLocked = false);
    }

    public void ChangeSky(SkyType targetSky)
    {
        if (switchLocked)
            return;

        switchLocked = true;

        TransitionSky(targetSky);
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
    private void ClearStarsky()
    {
        Transform starTransform = StarSky.transform;

        foreach (Transform child in starTransform)
        {
            Destroy(child.gameObject);
        }
    }
    public void SwitchTime()
    {
        if (currentHeight <= switchInterval)
        {
            return;
        }

        if (!switchLocked && currentHeight % switchInterval < switchInterval / 2)
        {
            if (!isNight)
            {
                isNight = true;
                ChangeSky(SkyType.NightSky);
                GenerateStarsky();
                EnableNightCloudSea();
            }
        }
        else if (!switchLocked && currentHeight % switchInterval >= switchInterval / 2)
        {
            if (isNight)
            {
                isNight = false;
                ChangeSky(SkyType.DaySky);
                EnableDayCloudSea();
                ClearStarsky();
            }
        }
    }

    public void EnableDayCloudSea()
    {
        NightCloudSea.GetComponent<CanvasGroup>().DOFade(0, 5f);
        DayCloudSea.GetComponent<CanvasGroup>().DOFade(1, 8f);
    }
    public void EnableNightCloudSea()
    {
        DayCloudSea.GetComponent<CanvasGroup>().DOFade(0, 5f);
        NightCloudSea.GetComponent<CanvasGroup>().DOFade(1, 8f);
    }
}
