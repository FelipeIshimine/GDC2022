using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseMonoSingleton<AudioManager>
{
    public AudioSource music;
    private void Awake()
    {
        music.Play();
    }
}
