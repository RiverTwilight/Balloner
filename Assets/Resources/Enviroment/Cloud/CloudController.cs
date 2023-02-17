using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CloudController : MoveableItem<CloudController>
{
    public List<Sprite> cloudImage;

    public GameObject imageContainer;

    public bool isFarCloud = true;

    private void Start()
    {
        GenerateCloud();
        ShuffleNearOrFar();
    }

    private void ShuffleNearOrFar()
    {
        float generalSpeed = GameObject.Find("Context").GetComponent<GameManager>().speed;

        if (isFarCloud)
        {
            float randomCloudDistance = Random.Range(0.2f, 1);

            transform.localScale = new Vector3(randomCloudDistance, randomCloudDistance, 1);

            GetComponent<CanvasGroup>().alpha = Random.Range(0.2f, 1);

            customSpeed = generalSpeed * randomCloudDistance;
        }
        else
        {
            transform.localScale = new Vector3(1.3f, 1.3f, 1f);
            GetComponent<CanvasGroup>().alpha = 0.9f;
            GetComponent<CloudController>().SetFarCloud(false);
            customSpeed = generalSpeed + 2f;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    [Button]
    private void GenerateCloud()
    {
        GetComponent<CanvasGroup>().alpha = Random.Range(0.2f, 1);
        imageContainer.GetComponent<Image>().sprite = cloudImage[Random.Range(0, cloudImage.ToArray().Length)];
    }

    public void SetFarCloud(bool status)
    {
        isFarCloud = status;
    }
}
