using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Checkbox : MonoBehaviour
{
    public Sprite checkedImage;
    public Sprite uncheckedImage;
    public bool isChecked;

    void Start()
    {
        isChecked = false;
        gameObject.GetComponent<Image>().sprite = uncheckedImage;
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
            gameObject.GetComponent<Image>().sprite = checkedImage;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = uncheckedImage;
        }
    }
}
