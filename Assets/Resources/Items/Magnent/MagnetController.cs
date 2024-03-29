using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MoveableItem
{
    public Animator animator;

    private void Start()
    {
        _item = new BoundedItem(() => Destroy(gameObject), ItemSet.Magnent, GetComponent<BoxCollider2D>());

        var reat = GetComponent<RectTransform>().rect;

        _item.itemSize = reat.size;
        _item.self = transform;

        // animator.speed = 0.5f;
        // animator.Play("Coin");
    }

    new public void HandleDestory()
    {
        _item.handleDestory();
    }
}
