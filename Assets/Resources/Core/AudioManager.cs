using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;

    [Title("Music")]
    public AudioClip[] musicClips;

    [Title("Sound")]
    public AudioClip coindClips;

    AudioSource musicSource;
    AudioSource soundSource;

    private void Awake()
    {
        current = this;

        //DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        soundSource = gameObject.AddComponent<AudioSource>();
    }

    public static async void PlayBackgroundMusic()
    {
        int index = Random.Range(0, current.musicClips.Length);

        current.musicSource.clip = current.musicClips[index];
        current.musicSource.loop = true;

        await UniTask.Delay(5000);

        current.musicSource.Play();
    }

    public static void StopBackgroundMusic()
    {
        if (current.musicSource != null)
        {
            current.musicSource.Stop();
        }
    }

    public static void PlaySoundEffect(ItemSet title)
    {
        switch (title)
        {
            case ItemSet.Coin_1:
                current.soundSource.PlayOneShot(current.coindClips);
                break;
            default:
                Debug.LogWarning("Invalid sound effect title: " + title);
                break;
        }
    }
}
