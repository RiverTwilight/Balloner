using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class SpliteGenerator : MonoBehaviour
{
    public GameObject group;
    public Sprite spiteImage;

    public float destoryTime = 5f;
    public float yMax = 6000;
    public float speed = .5f;

    public ItemManager.SpitePosition spitePosition;

    private void Start()
    {
        spitePosition = new ItemManager.SpitePosition(() => DestoryGroup());

        int spiteNum = Random.Range(0, 5);
        GenerateGroup(spiteNum);
    }

    private void Update()
    {
        MoveSpite();
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

    public void MoveSpite()
    {
        Vector3 v = transform.localPosition;
        transform.localPosition = new Vector3(v.x, Mathf.MoveTowards(v.y, -(Screen.height + 2000), speed), v.z);

        if (v.y < -(Screen.height + 1200))
        {
            //ItemManager.Instance.DestorySpite();
            //DestoryGroup();
        }
    }

    [Button]
    public void GenerateGroup(int spiteNum)
    {
        for (int i = 1; i <= spiteNum; i++)
        {
            var objTransform = CreateChild();

        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);

        var spiteRect = GetComponent<RectTransform>().rect;

        spitePosition.self = transform;

        spitePosition.spiteGroupSize = spiteRect.size;
    }

    public void DestoryGroup()
    {
        Destroy(gameObject);
    }
}
