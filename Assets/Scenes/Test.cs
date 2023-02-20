using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class Test : MonoBehaviour
{

    [Button]
    public void Set(int i)
    {
        PlayerPrefs.SetInt("Fxxk Unity", i);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("Fxxk Unity"));
    }
}
