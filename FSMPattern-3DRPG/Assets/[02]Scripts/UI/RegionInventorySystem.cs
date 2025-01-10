using System.Collections.Generic;
using UnityEngine;

public class RegionInventorySystem : MonoBehaviour
{
    private List<IItem> items = new List<IItem>();
    private IItemVisitor visitor;

    private void Start()
    {
        visitor = new SuwonVisitor();
    
        AddItem(new ArmorItem());
        AddItem(new WeaponItem());

        UpdateVisitorInfo();
    }

    void AddItem(IItem item)
    {
        items.Add( item);
    }

    void UpdateVisitorInfo()
    {
        foreach (var item in items)
        {
            item.Accept(visitor);
        }
    }

    private void Update()
    {
        ShowInfo();
    }

    public void ShowInfo()
    {
        foreach (var item in items)
        {
            item.ShowInfo();
        }
    }
}