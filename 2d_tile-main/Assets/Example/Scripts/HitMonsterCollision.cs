using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMonsterCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        HitCollision hi = other.GetComponent<HitCollision>();

        Vector3 backPosition = hi.transform.position - transform.position;
        backPosition.Normalize();

        hi.Knockback(backPosition, 100.0f);

        // rb.AddForce(backPosition * 800, ForceMode2D.Force);   
    }
}
