using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class SpliteGenerator : MoveableItem
{
    public GameObject group;
    public Sprite spiteImage;

    private void Start()
    {
        _item = new BoundedItem(() => DestoryGroup(), ItemSet.Spite, GetComponent<BoxCollider2D>());

        int spiteNum = Random.Range(3, 6);
        GenerateGroup(spiteNum);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public Transform CreateChild()
    {
        var obj = new GameObject();
        obj.transform.parent = transform;
        obj.transform.rotation = Quaternion.Euler(180, 0, 0);//Quaternion.FromToRotation(Vector3.zero, new Vector3(180, 0, 0));
        var img = obj.AddComponent<Image>();
        img.sprite = spiteImage;
        obj.transform.localScale = new Vector3(2, 2, 1);

        return obj.transform;
    }

    new public void HandleDestory()
    {
        _item.handleDestory();
    }

    [Button]
    public void GenerateGroup(int spiteNum)
    {
        for (int i = 1; i <= spiteNum; i++)
        {
            CreateChild();
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);

        var spiteRect = GetComponent<RectTransform>().rect;

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        boxCollider.size = new Vector2(spiteRect.size.x, spiteRect.size.y);

        _item.self = transform;

        _item.itemSize = new Vector2(spiteRect.size.x * 0.9f, spiteRect.size.y * 0.9f); // Not using actuall size but smaller
    }

    public void DestoryGroup()
    {
        Destroy(gameObject);
    }
}
