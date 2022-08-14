using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Range(0, 1)]
    public float volume;

    public AudioSource aus;

    public AudioClip[] bgMusic;

    public void PlayMusic(AudioClip ms, bool loop = true)
    {
        if(aus != null)
        {
            aus.loop = loop;
            aus.volume = volume;
            aus.clip = ms;
            aus.Play();
        }
    }

    public void PlayMusic(AudioClip[] ms, bool loop = true)
    {
        if (aus != null)
        {
            int index = Random.Range(0,ms.Length);
            if (ms[index] != null)
            {
                aus.loop = loop;
                aus.volume = volume;
                aus.clip = ms[index];
                aus.Play();
            }
        }
    }
    private void Start()
    {
        this.PlayMusic(bgMusic);
    }
}
