using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulties
{
    public Dictionary<string, float> Easy;
    public Dictionary<string, float> Medium;
    public Dictionary<string, float> Hard;

    public difficultySet currentLevel = difficultySet.Easy;
    public enum difficultySet
    {
        Easy,
        Medium,
        Hard
    }

    public void Awake()
    {
        Easy.Add("spiteSpeed", 15f);
        Medium.Add("spiteSpeed", 16f);
    }

    public float getData(string item)
    {
        Debug.Log(Easy);
        switch (currentLevel)
        {
            case difficultySet.Easy:
                return Easy[item];
        }
        return 0;
    }

    public void setLevel(difficultySet level)
    {
        currentLevel = level;
    }
}