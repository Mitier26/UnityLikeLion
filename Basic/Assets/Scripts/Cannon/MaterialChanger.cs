using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public GameObject cube;

    private void Start()
    {
        cube.GetComponent<MeshRenderer>().material.color = Color.gray;
        GetComponent<MeshRenderer>().material.color = Color.black;
    }

    private void Update()
    {
        BoxCollider boxCollider = cube.GetComponent<BoxCollider>();
        if (transform.position.x >= boxCollider.bounds.min.x || transform.position.z >= boxCollider.bounds.min.z ||
            transform.position.x <= boxCollider.bounds.max.x || transform.position.z <= boxCollider.bounds.max.z)
        {
            transform.position =
                new Vector3(Mathf.Clamp(transform.position.x, boxCollider.bounds.min.x, boxCollider.bounds.max.x), 0,
                    Mathf.Clamp(transform.position.z, boxCollider.bounds.min.z, boxCollider.bounds.max.z));
        }
        
        GetComponent<MeshRenderer>().material.color = new Color( Mathf.Abs(transform.position.x) / boxCollider.bounds.max.x, 0, Mathf.Abs(transform.position.z) / boxCollider.bounds.max.z);
        
    }
}
