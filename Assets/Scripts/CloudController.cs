using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CloudController : MonoBehaviour
{
    public float targetPosition;

    public Sprite cloudImage;

    public bool isFarCloud = true;

    private void Start()
    {
        targetPosition = -(Screen.height) - 2500;
        GenerateCloud();
    }

    private void Update()
    {
        Movement();
    }

    [Button]
    private void GenerateCloud()
    {
        GetComponent<CanvasGroup>().alpha = Random.Range(0.2f, 1);
    }

    public void SetFarCloud(bool status)
    {
        isFarCloud = status;
    }

    private void Movement()
    {

        Vector3 v = transform.localPosition;

        if (v.y <= targetPosition)
        {
            Destroy(gameObject);
        }

        GameManager gameManager = GameObject.Find("Context").GetComponent<GameManager>();

        float cloudSpeed = isFarCloud ? gameManager.speed / 10 - 0.5f : gameManager.speed / 10 + 1f;

        transform.localPosition = new Vector3(v.x, Mathf.MoveTowards(v.y, targetPosition, cloudSpeed), v.z);
    }
}
