using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

class operatorClass
{
    public int value;

    public static bool operator == (operatorClass op1, operatorClass op2)
    {
        return op2 != null && op1 != null && op1.value == op2.value;
    }

    public static bool operator != (operatorClass op1, operatorClass op2)
    {
        return !(op1 == op2);
    }
}


public class ItemGetter : MonoBehaviour
{
    public Inventory inventory;

    public RectTransform itemRectTransform;
    public Canvas canvas;
    public GameObject itemPrefab;

    public GameObject getEffectPrefab;

    public Camera camera;

    public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);


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
                boxTransform.position, curve.Evaluate(t / duration));

            itemTransform.position = newPosition;

            t += Time.deltaTime;
            yield return null;
        }

        itemTransform.position = boxTransform.position;

        inventory.AddItem(itemTransform.GetComponent<GettedObject>());
        Destroy(itemTransform.gameObject);

        var particle = Instantiate(getEffectPrefab, boxTransform.position, getEffectPrefab.transform.rotation);
        particle.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        var vector3 = particle.transform.position;
        vector3.z = 0.0f;
        particle.transform.position = vector3;
        Destroy(particle, 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        var newObject = Instantiate(itemPrefab,
            other.transform.position, Quaternion.identity, canvas.transform);
        newObject.GetComponent<GettedObject>().SetItemData(other.GetComponent<SpawnedItem>().itemData);
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
