using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class StarController : MonoBehaviour
{
    public List<Sprite> starImages;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    [Button]
    void Start()
    {
        var index = Random.Range(0, starImages.ToArray().Length);

        gameObject.GetComponent<Image>().sprite = starImages[index];

        //rectTransform = gameObject.GetComponent<RectTransform>();

        ShuffleSelfPostion();

        canvasGroup = gameObject.GetComponent<CanvasGroup>();

        Twinkle();
    }

    public void ShuffleSelfPostion() {
        var parentSize = transform.parent.gameObject.GetComponent<RectTransform>().rect.size;

        var randomPosY = Random.Range(10, parentSize.y);
        var randomPosX = Random.Range(10, parentSize.x);

        GetComponent<RectTransform>().anchoredPosition = new Vector3(randomPosX, randomPosY, 0);
    }

    async public void Twinkle()
    {
        while (true)
        {
            int randomDelay = Random.Range(1500, 2000);
            canvasGroup.DOFade(0, 0.6f);
            await UniTask.Delay(randomDelay);
            canvasGroup.DOFade(1, 0.6f);
            await UniTask.Delay(randomDelay);
        }
    }
}
