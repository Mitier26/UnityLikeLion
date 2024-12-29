using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance;
    public GameObject textPrefab;
    public int poolSize = 10;
    private Queue<GameObject> textPool = new Queue<GameObject>();
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            AddTextObject();
        }
    }
    
    private void AddTextObject()
    {
        GameObject textObj = Instantiate(textPrefab, transform);
        textObj.SetActive(false);
        textPool.Enqueue(textObj);
    }

    public void ShowDamageText(Vector3 position, float damage)
    {
        if (damage < 1) return;
        
        GameObject textObj;

        if (textPool.Count > 0)
        {
            textObj = textPool.Dequeue();
        }
        else
        {
            AddTextObject();
            
            textObj = textPool.Dequeue();
        }
        
        textObj.SetActive(true);
        
        TextMeshPro textMesh = textObj.GetComponent<TextMeshPro>();
        
        if (textMesh != null)
        {
            textMesh.text = damage.ToString("N0");
            textMesh.color = Color.red;
        }

        textObj.transform.position = position;
        
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(textObj.transform.DOMoveY(position.y + 1f , 0.25f).SetEase(Ease.OutCubic));
        sequence.Append(textObj.transform.DOMoveY(position.y - 1f , 0.25f).SetEase(Ease.InCubic));

        sequence.Join(DOTween.To(() => textMesh.color, x => textMesh.color = x, Color.black, 0.5f));
        
        sequence.OnComplete(()=> ReturnPool(textObj));

    }

    public void ReturnPool(GameObject textObj)
    {
        textObj.SetActive(false);
        textPool.Enqueue(textObj);
    }


}
