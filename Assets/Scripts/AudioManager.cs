using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

public class AudioManager : SingletonMonoBehavior<MonoBehaviour>
{
    static AudioManager current;

    [Title("Music")]
    public AudioClip[] musicClips;

    AudioSource musicSource;

    private void Awake()
    {
        current = this;

        //DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
    }

    public static async void playBackgroundMusic()
    {
        int index = Random.Range(0, current.musicClips.Length);

        current.musicSource.clip = current.musicClips[index];
        current.musicSource.loop = true;

        await UniTask.Delay(5000);

        current.musicSource.Play();
    }

    public static void stopBackgroundMusic()
    {
        if (current.musicSource != null)
        {
            current.musicSource.Stop();
        }
    }
}
