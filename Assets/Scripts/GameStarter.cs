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
    public CanvasGroup Score;
    public CanvasGroup HintText;
    public GameObject Context;

    public float ballonAppearDelay;
    private bool shakeHintText = true;

    private void Start()
    {
        GetComponent<InteractableMonoBehavior>().onPointerClick.AddListener((eD) =>
        {
            if (Context.GetComponent<GameManager>().gameStatus == GameManager.GameStatusSet.Initialized)
            {
                UIcanvasGroup.DOFade(0, 0.5f);
                Score.DOFade(1, 0.5f);
                Ballon.DOAnchorPosY(786, ballonAppearDelay).SetEase(Ease.InCubic);
                Context.GetComponent<GameManager>().StartGame();
                shakeHintText = false;
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
