using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class TimeManager : MonoBehaviour
{
    public bool isNight = false;
    public bool switchLocked = false;
    public int switchInterval = 100;

    //public GameObject Background;

    // Start is called before the first frame update
    //void Start()
    //}

    //// Update is called once per frame
    void Update()
    {
        SwitchTime();
    }

    public void SwitchTime()
    {
        int currentHeight = gameObject.GetComponent<GameManager>().currentHeight;

        if (currentHeight > switchInterval && currentHeight % switchInterval > switchInterval / 2)
        {
            switchLocked = false;
        }

        if (!switchLocked && currentHeight > switchInterval && currentHeight % switchInterval <= 10)
        {
            isNight = !isNight;
            switchLocked = true;
        }
    }
}
