using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 스피커, 헤드셋, 소리 파일
    private AudioSource audioSource; // 스피커
    public AudioClip[] clips; // 0. 인트로 / 1. 메인 / 2. 점프 / 3. 충돌

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

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

    public void OnJumpSound() // 1번만 실행하는 Event 사운드
    {
        audioSource.PlayOneShot(clips[2]);
    }

    public void OnCollisionSound() // 1번만 실행하는 Event 사운드
    {
        audioSource.PlayOneShot(clips[3]);
    }
}