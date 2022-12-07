using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Image aImg;
    public Image bImg;

    public Bounds aBounds;
    public Bounds bBounds;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        aBounds = new Bounds(aImg.transform.position, aImg.GetComponent<RectTransform>().rect.size);
        bBounds = new Bounds(bImg.transform.position, bImg.GetComponent<RectTransform>().rect.size);

        if (aBounds.Intersects(bBounds))
        {
            Debug.Log("asfasdfasfwse");
        }
    }
}
