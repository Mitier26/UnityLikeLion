using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemGetter : MonoBehaviour
{
    public RectTransform itemRectTransform;
    public Canvas canvas;
    public GameObject itemPrefab;

    public GameObject getEffectPrefab;

    public Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // 이징 함수
    private float EaseInOut(float t)
    {
        return t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;
    }

    IEnumerator GoingToBox(RectTransform itemTransform, RectTransform boxTransform)
    {
        float duration = 2.0f;
        float t = 0.0f;

        Vector3 itemBeginPOS = itemTransform.position;
        
        while (1.0f >= t / duration)
        {
            Vector3 newPosition = Vector3.Lerp(itemBeginPOS, 
                boxTransform.position, EaseInOut(t / duration));

            itemTransform.position = newPosition;
            
            t += Time.deltaTime;
            yield return null;
        }

        itemTransform.position = boxTransform.position;
        Destroy(itemTransform.gameObject);
        
        var particle = Instantiate(getEffectPrefab, boxTransform.position,getEffectPrefab.transform.rotation);
        particle.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        var vector3 = particle.transform.position;
        vector3.z = 0.0f;
        particle.transform.position = vector3;
        Destroy(particle, 3.0f);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        
        var newObject = Instantiate(itemPrefab, 
            other.transform.position, Quaternion.identity, canvas.transform);
        newObject.GetComponent<Image>().sprite = other.GetComponent<SpriteRenderer>().sprite;
        newObject.transform.position = other.transform.position;
        var newScreenPosition = Camera.main.WorldToScreenPoint(newObject.transform.position);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(), newScreenPosition, camera, out localPoint);
        newObject.transform.localPosition = localPoint;
        
        StartCoroutine(GoingToBox(newObject.GetComponent<RectTransform>(), itemRectTransform));
        
        
        Destroy(other.gameObject);
    }
}
