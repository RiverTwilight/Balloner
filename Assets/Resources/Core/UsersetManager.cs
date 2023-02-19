using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersetManager : MonoBehaviour
{
    public Checkbox MusicSetCheckbox;

    // Start is called before the first frame update
    void Start()
    {
        //MusicSetCheckbox.onClick.AddListener((title, state) =>
        //{
        //    PlayerPrefs.SetInt(title, state ? 1 : 0);
        //});
    }

    public void handleCheckboxUpdate(string title, bool state)
    {
        Debug.Log(state);
        PlayerPrefs.SetInt(title, state ? 1 : 0);
    }

    void Update()
    {

    }
}
