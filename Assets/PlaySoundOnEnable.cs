using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnEnable : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnEnable()
    {
        audioSource.Play();
    }
}
