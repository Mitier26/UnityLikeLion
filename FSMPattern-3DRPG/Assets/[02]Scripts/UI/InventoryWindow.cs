using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIComponent
{
    void Initialize();
    void SetActive(bool active);
    void UpdateUI();
    void ResetState();
    void Cleanup();
}

public class InventoryWindow : MonoBehaviour, IUIComponent
{
    private List<IUIComponent> components = new List<IUIComponent>();

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        components.Add(GetComponent<StatsPanel>());
        components.Add(GetComponent<ItemListPanel>());

        foreach (IUIComponent uiComponent in components)
        {
            uiComponent.Initialize();
        }
    }

    public void SetActive(bool active)
    {
        foreach (IUIComponent uiComponent in components)
        {
            uiComponent.SetActive(active);
        }
        
        gameObject.SetActive(active);
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (var uiComponent in components)
        {
            uiComponent.UpdateUI();
        }
    }

    public void ResetState()
    {
        foreach (var uiComponent in components)
        {
            uiComponent.ResetState();
        }
    }

    public void Cleanup()
    {
        foreach (var uiComponent in components)
        {
            uiComponent.Cleanup();
        }
    }
}
