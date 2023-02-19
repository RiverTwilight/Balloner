using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

public class UsersetManager : MonoBehaviour
{
    [Button]
    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public GameObject SoundSetCheckbox;
    public GameObject MusicSetCheckbox;

    void Start()
    {
        if (PlayerPrefs.GetInt("Initialized") != 1)
        {
            Debug.Log("First Loading");
            InitializeSetting();
        }
        InitiializeList();
    }

    public void handleCheckboxUpdate(string title, bool state)
    {
        Debug.Log("Set" + title + "to" + state);
        PlayerPrefs.SetInt(title, state ? 1 : 0);

        Debug.Log("Current " + title + "is" + PlayerPrefs.GetFloat(title));
    }

    private void InitializeSetting()
    {
        PlayerPrefs.SetInt("EnableMusic", 1);
        PlayerPrefs.SetInt("EnableSound", 1);
        PlayerPrefs.SetInt("Initialized", 1);
    }

    private void InitiializeList()
    {
        MusicSetCheckbox.GetComponent<Checkbox>().ForceSetState(PlayerPrefs.GetFloat("EnableMusic") == 1);
        SoundSetCheckbox.GetComponent<Checkbox>().ForceSetState(PlayerPrefs.GetFloat("EnableSound") == 1);
    }

    //void Update()
    //{

    //}
}
