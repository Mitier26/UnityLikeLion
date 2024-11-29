using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeEvent : MonoBehaviour
{
    public SoundManager soundManager;
    public GameObject endUI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<BirdMovement>().OnParticle();
            other.GetComponent<BirdMovement>().isDead = true;
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            endUI.SetActive(true);
            soundManager.OnCollisionSound();
            
        }
    }
}
