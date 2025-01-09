using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIPanel
{
    void Show();
    void Hide();
    void Update(float deltaTime);
}

public class InventoryFacadePattern 
{
    public void SortInventory()
    {
        
    }

    public void Show()
    {
        
    }

    public void Add()
    {
        
    }
}

public class StatusFacadePattern 
{
    public int GetShareHp()
    {
        return 0;
    }
}

public class ShopFacadePattern 
{
    public void Buy()
    {
        
    }
}

public class UIFacade : MonoBehaviour
{
    private IUIPanel uiCurrentPanel;
    
    private InventoryFacadePattern inventory;
    private StatusFacadePattern status;
    private ShopFacadePattern shop;

    private void Start()
    {
        inventory = new InventoryFacadePattern();
        status = new StatusFacadePattern();
        shop = new ShopFacadePattern();
        
    }

    public void Show()
    {
        shop.Buy();
        inventory.Add();
        status.GetShareHp();
        inventory.Show();
    }

}
