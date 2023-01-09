using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class StarController : MonoBehaviour
{
    public List<Sprite> starImages;

    // Start is called before the first frame update
    [Button]
    void Start()
    {
        var index = Random.Range(0, starImages.ToArray().Length);

        gameObject.GetComponent<Image>().sprite = starImages[index];

        var parentSize = transform.parent.gameObject.GetComponent<RectTransform>().rect.size;

        var randomPosY = Random.Range(10, parentSize.y);
        var randomPosX = Random.Range(10,parentSize.x);

        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(randomPosX, randomPosY, 0);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
