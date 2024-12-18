using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CButton : MonoBehaviour
{
    [SerializeField] private Button button;
    
    public int Index { get; set; }

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void AddListener(UnityAction<int> listener)
    {
        button.onClick.AddListener(()=>listener(Index));
    }

    public void RemoveListener(UnityAction listener) 
    {
        button.onClick.RemoveListener(listener);
    }
}
