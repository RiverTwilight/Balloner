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
    public bool isChecked = false;
    [HideLabel] public UnityEvent<string, bool> onClick;
    public string title;
    public Image IconSlot;

    public void Start()
    {
        onClick.RemoveAllListeners();
    }

    public void ForceSetState(bool state)
    {
        Debug.Log("Force Set state:" + title + " " + state);
        isChecked = state;
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        if (isChecked)
        {
            IconSlot.sprite = checkedImage;
        }
        else
        {
            IconSlot.sprite = uncheckedImage;
        }
    }

    public void ToggleState()
    {
        isChecked = !isChecked;
        UpdateSprite();
        onClick.Invoke(title, isChecked);
    }
}
