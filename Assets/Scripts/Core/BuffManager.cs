using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public List<Buff> buffs;

    void Start()
    {
        buffs = new List<Buff>();
    }

    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = 0; i < buffs.Count; ++i)
        {
            buffs[i].Duration -= deltaTime;
            if (buffs[i].Duration <= 0)
            {
                buffs.RemoveAt(i);
                --i;
            }
        }
    }

    public bool HasBuff(string name)
    {
        return buffs.Exists(buff => buff.Name == name);
    }
}
