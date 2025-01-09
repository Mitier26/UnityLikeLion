using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanel : MonoBehaviour, IUIComponent
{
    public int Hp;
    public int Atk;
    
    public void Initialize()
    {
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void UpdateUI()
    {
        Debug.Log($"{Hp}, {Atk}");
    }

    public void ResetState()
    {
    }

    public void Cleanup()
    {
    }
}
