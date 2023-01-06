using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CoinController : MonoBehaviour
{
    public float speed;
    public float targetPosition;

    public Animator animator;

    private void Start()
    {
        animator.speed = 0.5f;
        animator.Play("Coin");
        targetPosition = -(Screen.height) - 3000;

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {

        Vector3 v = transform.localPosition;

        if (v.y <= targetPosition)
        {
            Destroy(gameObject);
        }

        GameManager gameManager = GameObject.Find("Context").GetComponent<GameManager>();

        transform.localPosition = new Vector3(v.x, Mathf.MoveTowards(v.y, targetPosition, gameManager.speed), v.z);
    }
}
