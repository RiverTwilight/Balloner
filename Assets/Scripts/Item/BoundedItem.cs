using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoundedItem
{
    public Transform self;
    public Vector3 itemSize;
    public bool collected = false;
    public UnityAction handleDestory;
    public ItemSet itemType;

    public BoxCollider2D boxCollider;

    public BoundedItem(UnityAction handleDestory, ItemSet itemType, BoxCollider2D boxCollider)
    {
        this.handleDestory = handleDestory;
        this.itemType = itemType;
        this.boxCollider = boxCollider;
    }
    public Bounds CreateBounds()
    {
        return new Bounds(boxCollider.bounds.center, boxCollider.bounds.size);
    }
}
