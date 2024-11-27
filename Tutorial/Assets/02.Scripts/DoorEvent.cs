using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    public Animator animator;
    void AnimationEvent()
    {
        Debug.Log("문이 열리고 있는 중");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Open");
            Debug.Log(other.name + "무엇인가 감지되었다");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Close");
            Debug.Log(other.name + "감지되던 것이 사라졌다");
        }
    }
}
