using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CoinController : MoveableItem<CloudController>
{
    public Animator animator;

    public ItemManager.Item coinPosition;
    public ItemSet coinType;

    private void Start()
    {
        coinPosition = new ItemManager.Item(() => Destroy(gameObject), coinType);

        var coinRect = GetComponent<RectTransform>().rect;

        coinPosition.itemSize = coinRect.size;
        coinPosition.self = transform;

        animator.speed = 0.5f;
        animator.Play("Coin");
    }

    new public void HandleDestory()
    {
        coinPosition.handleDestory();
    }

    private void FixedUpdate()
    {
        Move();
    }
}
