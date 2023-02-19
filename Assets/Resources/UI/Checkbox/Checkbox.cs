using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class Checkbox : MonoBehaviour
{
    public Sprite checkedImage;
    public Sprite uncheckedImage;
    public bool isChecked;
    [HideLabel] public UnityEvent<string, bool> onClick;
    public string title;
    public Image IconSlot;
    //public event StateChanged OnStateChanged;

    public void Start()
    {
        isChecked = false;
        IconSlot.sprite = uncheckedImage;
        onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toggleState()
    {
        isChecked = !isChecked;

        if (isChecked)
        {
            IconSlot.sprite = checkedImage;
        }
        else
        {
            IconSlot.sprite = uncheckedImage;
        }

        onClick.Invoke(title, isChecked);
    }
}
