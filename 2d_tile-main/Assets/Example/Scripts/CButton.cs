using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CButton : MonoBehaviour
{
    [SerializeField] private Button button;


    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void AddListener(UnityAction listener)
    {
        button.onClick.AddListener(listener);
    }

    public void RemoveListener(UnityAction listener) 
    {
        button.onClick.RemoveListener(listener);
    }
}
