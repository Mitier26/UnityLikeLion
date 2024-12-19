using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudionManager : MonoBehaviour
{
    public static AudionManager instance;
    
    public AudioSource audioSource;
    
    public AudioClip[] audioClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void BarSound()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }

    public void BlockSound()
    {
        audioSource.PlayOneShot(audioClips[1]);
    }
    
    public void WallSound()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }
    
}
