using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEditor;

public class BSpiteController : MoveableItem
{
    private void Start()
    {
        _item = new BoundedItem(() => DestoryGroup(), ItemSet.Spite, GetComponent<BoxCollider2D>());
        _item.self = transform;

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            // Get size from BoxCollider2D.
            _item.itemSize = new Vector2(boxCollider.size.x * 0.9f, boxCollider.size.y * 0.9f);
        }
        else
        {
            // Fallback to getting size from RectTransform.
            var spiteRect = GetComponent<RectTransform>().rect;
            _item.itemSize = new Vector2(spiteRect.size.x * 0.9f, spiteRect.size.y * 0.9f);
        }
    }

    new public void HandleDestory()
    {
        _item.handleDestory();
    }

    public void DestoryGroup()
    {
        Destroy(gameObject);
    }
}
