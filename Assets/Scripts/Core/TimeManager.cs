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

    [Title("Game Objects")]
    public Image SkyContainer;
    public Image SkyBackContainer;
    public Image DayCloudSea;
    public Image NightCloudSea;
    public Image Moon;
    public GameObject NearCloudsea;
    public GameObject StarSky;

    public GameObject Star_Prefab;

    public Sprite DaySky;
    public Sprite LowDaySky;
    public Sprite NightSky;

    private Dictionary<SkyType, Sprite> skySprites = new Dictionary<SkyType, Sprite>();
    [ReadOnly] public bool dayNightCycleStarted = false;

    public int starPoolSize = 7;
    private List<GameObject> starPool;
    private CanvasGroup dayCloudSeaCanvasGroup;
    private CanvasGroup nightCloudSeaCanvasGroup;
    void Awake()
    {
        gameManager = gameObject.GetComponent<GameManager>();

        skySprites[SkyType.DaySky] = DaySky;
        skySprites[SkyType.LowDaySky] = LowDaySky;
        skySprites[SkyType.NightSky] = NightSky;

        dayCloudSeaCanvasGroup = DayCloudSea.GetComponent<CanvasGroup>();
        nightCloudSeaCanvasGroup = NightCloudSea.GetComponent<CanvasGroup>();
    }

    void FixedUpdate()
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

    void Start()
    {
        starPool = new List<GameObject>();

        for (int i = 0; i < starPoolSize; i++)
        {
            GameObject star = Instantiate(Star_Prefab, StarSky.transform);
            star.SetActive(false);
            starPool.Add(star);
        }
    }

    private void TransitionSky(SkyType targetSky)
    {
        float duration = 4f;

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
        Moon.DOFade(1, 4f);
        foreach (GameObject star in starPool)
        {
            star.GetComponent<StarController>().ShuffleSelfPostion();
            star.SetActive(true);
        }
    }

    private void ClearStarsky()
    {
        Moon.DOFade(0, 4f);
        foreach (GameObject star in starPool)
        {
            star.SetActive(false);
        }
    }


    public void SwitchTime()
    {
        if (currentHeight <= switchInterval)
        {
            return;
        }

        if (!switchLocked)
        {
            if (currentHeight % switchInterval < switchInterval / 2)
            {
                if (!isNight)
                {
                    isNight = true;
                    ChangeSky(SkyType.NightSky);
                    GenerateStarsky();
                    EnableNightCloudSea();
                }

            }
            else if (currentHeight % switchInterval >= switchInterval / 2)
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
    }

    public void EnableDayCloudSea()
    {
        nightCloudSeaCanvasGroup.DOFade(0, 5f);
        dayCloudSeaCanvasGroup.DOFade(1, 8f);
    }

    public void EnableNightCloudSea()
    {
        dayCloudSeaCanvasGroup.DOFade(0, 5f);
        nightCloudSeaCanvasGroup.DOFade(1, 8f);
    }
}
