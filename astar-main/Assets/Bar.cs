using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public int _data;
    public bool bResizeFinish;
    
    public TextMeshPro _text;
    public GameObject _stick;

    void Start()
    {
        _text.text = _data.ToString();
    }
    
    public void SetData(int index)
    {
        StartCoroutine(Lerp(index));
    }

    IEnumerator Lerp(int index)
    {
        bResizeFinish = false;
        
        float t = 0.0f;
        float duration = 0.3f;
        while (true)
        {
            t += Time.deltaTime;
            if (t >= duration)
            {
                break;
            }
            
            _stick.transform.position = Vector3.Lerp(_stick.transform.position, new Vector3(index, _data / 2.0f, 0), t / duration);
            _stick.transform.localScale = Vector3.Lerp(_stick.transform.localScale, new Vector3(1,_data, 1), t / duration);

            _text.transform.position = _stick.transform.position + new Vector3(0,0,-3.0f);;
            yield return null;
        }
        
        _stick.transform.position = new Vector3(index, _data / 2.0f, 0);
        _stick.transform.localScale = new Vector3(1,_data, 1);
        _text.transform.position = _stick.transform.position + new Vector3(0,0,-3.0f);
        
        bResizeFinish = true;
    }
}
