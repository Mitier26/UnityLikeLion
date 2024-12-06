using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectCreator : MonoBehaviour
{
    public GameObject prefab;
    public int createCount;
    public List<GameObject> objects = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < createCount; i++)
            {
                float x = Random.Range(-100, 100);
                float y = Random.Range(-100, 100);
                float z = Random.Range(-100, 100);
            
                GameObject ob = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
                objects.Add(ob);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Delete))
        {
            for (int i = 0; i < createCount; i++)
            {
                Destroy(objects[i]);
            }
            
            objects.Clear();
        }
    }
}
