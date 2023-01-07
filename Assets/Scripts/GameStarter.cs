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
    public RectTransform Land;
    public RectTransform Mountain;
    public CanvasGroup Score;
    public CanvasGroup HintText;
    public GameObject Context;

    [Title("Movement")]
    public float ballonLiftDelay;
    public float landDownDelay;
    public float mountainDownDelay;

    private bool shakeHintText = true;

    private void Start()
    {
        GetComponent<InteractableMonoBehavior>().onPointerClick.AddListener((eD) =>
        {
            if (Context.GetComponent<GameManager>().gameStatus == GameStatusSet.Initialized)
            {
                UIcanvasGroup.DOFade(0, 0.5f);
                Score.DOFade(1, 0.5f);
                Ballon.DOAnchorPosY(800, ballonLiftDelay).SetEase(Ease.InCubic);
                Land.DOAnchorPosY(-3300, landDownDelay).SetEase(Ease.InCubic);
                Mountain.DOAnchorPosY(-1000, mountainDownDelay).SetEase(Ease.InCubic);
                Context.GetComponent<GameManager>().StartGame();
                shakeHintText = false;
                AudioManager.playBackgroundMusic();
            }
        });
        ShakeHintText();
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
