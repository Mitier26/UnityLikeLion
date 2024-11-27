using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEvent : MonoBehaviour
{
    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<CharacterMovement>().isGround = true;
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<CharacterMovement>().isGround = false;
        }
    }
}
