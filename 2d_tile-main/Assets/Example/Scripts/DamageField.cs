using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageField : MonoBehaviour
{
    public string MyOwnerTag;
    public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(MyOwnerTag))
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Monster>().Hp -= damage;
            }
        }
    }
}
