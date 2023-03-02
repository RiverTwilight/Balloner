using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudseaController : MoveableItem
{
    public bool autoDestory;
    public int targetY;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        var rectTransform = gameObject.GetComponent<RectTransform>();

        Vector3 v = rectTransform.anchoredPosition;

        if(v.y < targetY + 2 && autoDestory)
        {
            Destroy(gameObject);
        }

        GameManager gameManager = GameObject.Find("Context").GetComponent<GameManager>();

        float activeSpeed = gameManager.speed;

        rectTransform.anchoredPosition = new Vector3(v.x, Mathf.MoveTowards(v.y, targetY, activeSpeed), v.z);
    }
}
