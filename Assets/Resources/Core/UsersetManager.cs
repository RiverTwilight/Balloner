using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersetManager : MonoBehaviour
{
    //void Start()
    //{
    //}

    public void handleCheckboxUpdate(string title, bool state)
    {
        PlayerPrefs.SetInt(title, state ? 1 : 0);
    }

    //void Update()
    //{

    //}
}
