using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rorator : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}