using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

public class GameStarter : MonoBehaviour
{
    public CanvasGroup UIcanvasGroup;

    [Title("Game Objects")]
    public RectTransform Ballon;
    public RectTransform Ground;
    public RectTransform BallonShadow;
    public RectTransform FarMountain;
    public RectTransform NearMountain;

    [Title("In-game UI Objects")]
    public CanvasGroup Score;
    public CanvasGroup HintText;
    public GameObject PauseButton;
    public GameObject Context;

    [Title("Movement")]
    public float ballonLiftDelay;
    public float landDownDelay;
    public float nearMountainDownDelay;
    public float farMountainDownDelay;

    private bool shakeHintText = true;

    private void Start()
    {
        GetComponent<InteractableMonoBehavior>().onPointerClick.AddListener((eD) =>
        {
            GameTrigger();
        });
        //ShakeHintText();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GameTrigger();
        }
    }

    private async void GameTrigger()
    {
        if (Context.GetComponent<GameManager>().gameStatus == GameStatusSet.Initialized)
        {
            UIcanvasGroup.DOFade(0, 0.5f);
            Score.DOFade(1, 0.5f);
            Ballon.DOAnchorPosY(700, ballonLiftDelay).SetEase(Ease.InCubic);
            BallonShadow.DOScale(new Vector3(0, 0, 1), 3);
            Ground.DOAnchorPosY(-1000, landDownDelay).SetEase(Ease.InCubic);
            NearMountain.DOAnchorPosY(-1000, nearMountainDownDelay).SetEase(Ease.InCubic);
            FarMountain.DOAnchorPosY(-1000, farMountainDownDelay).SetEase(Ease.InCubic);

            PauseButton.SetActive(true);
            shakeHintText = false;

            if (PlayerPrefs.GetInt("EnableMusic") == 1)
            {
                AudioManager.PlayBackgroundMusic();
            }
            
            await UniTask.DelayFrame(0);
            Context.GetComponent<GameManager>().StartGame();
        }
    }
    async public void ShakeHintText()
    {
        while (shakeHintText)
        {
            HintText.DOFade(0, 0.6f);
            await UniTask.Delay(600);
            HintText.DOFade(1, 0.6f);
            await UniTask.Delay(600);
        }
    }
}
