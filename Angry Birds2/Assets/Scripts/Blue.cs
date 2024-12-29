using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blue : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public float damage = 10f;
    public BirdState birdState = BirdState.Flying;


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Block>(out Block block))
        {
            block.TakeDamage(other.contacts[0].point, damage);   
        }
    }
}
