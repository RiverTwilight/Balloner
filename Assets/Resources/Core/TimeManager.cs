using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;

public class TimeManager : MonoBehaviour
{
    [Title("Time Status")]
    public bool isNight = false;
    [ReadOnly] public bool reachCloudsea = false;
    [ReadOnly] public bool switchLocked = false;

    [Range(10, 1000)]
    public int switchInterval = 100;
    public int currentHeight;
    public int cloudseaHeight;

    public GameManager gameManager;

    public GameObject NightBackground;
    public GameObject DayBackground;
    public GameObject FarCloudsea;
    public GameObject NearCloudsea;

    public GameObject StarSky;

    public GameObject Star_Prefab;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
    }

    //// Update is called once per frame
    void Update()
    {
        currentHeight = gameManager.currentHeight;

        SwitchTime();

        if (currentHeight > cloudseaHeight && !reachCloudsea)
        {
            PlayCrossCloud();
        }
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

    public void PlayCrossCloud()
    {
        reachCloudsea = true;
        NearCloudsea.SetActive(true);
        FarCloudsea.SetActive(true);
        FarCloudsea.GetComponent<CanvasGroup>().DOFade(1, 8f);
    }
}
