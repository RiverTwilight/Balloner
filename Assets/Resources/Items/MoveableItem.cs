using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MoveableItem : MonoBehaviour
{
    [ReadOnly] public float customSpeed = 0;
    public ItemManager.Item _item;


    public void Move()
    {
        var rectTransform = gameObject.GetComponent<RectTransform>();

        // ê��ͳһ�����ڵײ�
        Vector3 v = rectTransform.anchoredPosition;

        if (v.y < -500)
        {
            HandleDestory();
        }

        float activeSpeed;

        if (customSpeed > 0)
        {
            activeSpeed = customSpeed;
        }
        else
        {
            GameManager gameManager = GameObject.Find("Context").GetComponent<GameManager>();

            activeSpeed = gameManager.speed;
        }

        rectTransform.anchoredPosition = new Vector3(v.x, Mathf.MoveTowards(v.y, -1000, activeSpeed), v.z);
    }

    public void HandleDestory()
    {
        Destroy(gameObject);
    }
}
