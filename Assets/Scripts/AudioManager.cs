using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;

    [Title("Music")]
    public AudioClip musicClip;

    private void Awake()
    {
        
    }
}
