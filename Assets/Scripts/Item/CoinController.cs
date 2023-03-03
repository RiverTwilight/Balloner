using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CoinController : MoveableItem
{
    public Animator animator;

    private void Start()
    {
        
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

    private void FixedUpdate()
    {
        Move();
    }
}
