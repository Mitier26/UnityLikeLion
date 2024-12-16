using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGetter : MonoBehaviour
{
    public RectTransform itemRectTransform;
    public Canvas canvas;
    public GameObject itemPrefab;

    public Camera camera;
    
    private float EaseInOut(float t)
    {
        return t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;
    }

    IEnumerator GoingTBox(RectTransform itemTransform, RectTransform boxTransform)
    {
        float duration = 1.0f;
        float t = 0.0f;

        Vector3 itemBeginPOS = itemTransform.position;
        
        while (1.0f >= t / duration)
        {
            Vector3 newPosition = Vector3.Lerp(itemBeginPOS, 
                boxTransform.position, t / duration);

            itemTransform.position = newPosition;
            
            t += Time.deltaTime;
            yield return null;
        }

        itemTransform.position = boxTransform.position;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        
        var newObject = Instantiate(itemPrefab, other.transform.position, Quaternion.identity, canvas.transform);
        newObject.GetComponent<Image>().sprite = other.GetComponent<SpriteRenderer>().sprite;
        newObject.transform.position = other.transform.position;
        var newScreenPosition = Camera.main.WorldToScreenPoint(newObject.transform.position);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), newScreenPosition, camera, out localPoint);
        newObject.transform.localPosition = localPoint;
        
        StartCoroutine(GoingTBox(newObject.GetComponent<RectTransform>(), itemRectTransform));
        
        
        Destroy(other.gameObject);
    }
}
