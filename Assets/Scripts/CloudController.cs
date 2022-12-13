using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CloudController : MonoBehaviour
{
    public float speed;
    public float targetPosition;

    public Sprite cloudImage;

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

    private void Movement()
    {
        Vector3 v = transform.localPosition;

        if (v.y <= targetPosition)
        {
            Destroy(gameObject);
        }

        transform.localPosition = new Vector3(v.x, Mathf.MoveTowards(v.y, targetPosition, speed), v.z);

    }
}
