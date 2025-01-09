using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListPanel : MonoBehaviour, IUIComponent
{
    public void Initialize()
    {
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void UpdateUI()
    {
        Debug.Log("ㅁㅁㅁㅁㅁㅁ");
        Debug.Log("ㅁㅁㅁㅁㅁㅁ");
        Debug.Log("ㅁㅁㅁㅁㅁㅁ");
        Debug.Log("ㅁㅁㅁㅁㅁㅁ");
    }

    public void ResetState()
    {
        
    }

    public void Cleanup()
    {
        
    }
}
