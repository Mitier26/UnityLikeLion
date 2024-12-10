using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    public GameObject prefab;
    public int CreateCount;
    public List<GameObject> objects = new List<GameObject>();
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < CreateCount; i++)
            {
                float x = Random.Range(-100, 100);
                float y = Random.Range(-100, 100);
                float z = Random.Range(-100, 100);
            
                var go = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
                objects.Add(go);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Delete))
        {
            for (var i = 0; i < objects.Count; i++)
            {
                Destroy(objects[i]);
            }
            
            objects.Clear();
        }
    }
}
