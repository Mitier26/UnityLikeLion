using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        // 시작 시 인트로 노래 재생
        OnIntroBGM();
    }

    public void OnIntroBGM()
    {
        audioSource.clip = clips[0];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void OnMainBGM()
    {
        audioSource.clip = clips[1];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void OnJumpSound()
    {
        audioSource.PlayOneShot(clips[2]);
        // 메인 소리가 끊어지지 않는다.
    }

    public void OnCollisionSound()
    {
        audioSource.PlayOneShot(clips[3]);
    }
}
