using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CoinController : MoveableItem
{
    public Animator animator;

    private Player player;
    private bool attracted = false;
    public float attractionSpeed = 5f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        _item = new BoundedItem(() => Destroy(gameObject), Random.Range(0, 100) <= 70 ? ItemSet.Coin_1 : ItemSet.Coin_10, GetComponent<BoxCollider2D>());

        var coinRect = GetComponent<RectTransform>().rect;

        _item.itemSize = coinRect.size;
        _item.self = transform;

        animator.speed = 0.5f;
        animator.Play("Coin");
    }

    new public void HandleDestory()
    {
        _item.handleDestory();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (attracted)
        {
            Debug.Log("Being attarcting");
            Attract();
        }
        else {
            Move();
        }
    }

    public void StartAttract()
    {
        attracted = true;
    }

    private void Attract()
    {
        var direction = (player.transform.position - transform.position).normalized;
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += (Vector2)(direction * attractionSpeed * Time.deltaTime);
    }

    // Call this method when the magnet effect finishes
    public void StopAttract()
    {
        attracted = false;
    }
}
