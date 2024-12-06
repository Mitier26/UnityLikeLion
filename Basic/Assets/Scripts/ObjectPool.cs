// ObjectPool.cs

using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;
    public int CreateCount;

    private Queue<GameObject> objectPool = new Queue<GameObject>();
    public List<GameObject> objects = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            return objectPool.Dequeue();
        }
        return null;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < CreateCount; i++)
            {
                float x = Random.Range(-100, 100);
                float y = Random.Range(-100, 100);
                float z = Random.Range(-100, 100);

                var go = GetPooledObject();
                go.transform.position = new Vector3(x, y, z);
                objects.Add(go);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Delete))
        {
            for (var i = 0; i < objects.Count; i++)
            {
                ReturnToPool(objects[i]);
            }
            
            objects.Clear();
        }
    }
}