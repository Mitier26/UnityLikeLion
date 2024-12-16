using System;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}
