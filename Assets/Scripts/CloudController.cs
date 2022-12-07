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
        targetPosition = -(Screen.height) - 3000;
        GenerateCloud();
    }
    // Update is called once per frame
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
        transform.localPosition = new Vector3(v.x, Mathf.MoveTowards(v.y, targetPosition, speed), v.z);

        //Debug.Log(v.y);

        if (v.y < targetPosition - 200)
        {
            Destroy(gameObject);
        }
    }
}
