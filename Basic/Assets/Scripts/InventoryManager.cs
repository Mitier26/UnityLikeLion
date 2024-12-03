using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private LinkedList<Item> inventory;

    private void Start()
    {
        inventory = new LinkedList<Item>();
        InitializeInventory();
        PrintInventory();

        AddItem("포션", 2);
        PrintInventory();
        RemoveItem("검");
        PrintInventory();
    }

    void InitializeInventory()
    {
        AddItem("검", 1);
        AddItem("방패", 1);
        AddItem("물약", 5);
    }
    
    void AddItem(string name, int quantity)
    {
        LinkedListNode<Item> node = inventory.First;

        while (node != null)
        {
            if (node.Value.Name == name)
            {
                node.Value.Quantity += quantity;
                Debug.Log($"Updated {name} quantity. New quantity : {node.Value.Quantity}");
                return;
            }

            node = node.Next;
        }

        inventory.AddLast(new Item(name, quantity));
        Debug.Log($"Added new Item : {name} (quantity: {quantity})");
    }

    void RemoveItem(string name)
    {
        LinkedListNode<Item> node = inventory.First;
        while (node != null)
        {
            if (node.Value.Name == name)
            {
                inventory.Remove(node);
                Debug.Log($"Removeed item: {name}");
                return;
            }
            node = node.Next;
        }
        Debug.Log($"Item not Found: {name}");
    }

    void PrintInventory()
    {
        Debug.Log("Current Inventory : ");
        foreach (Item item in inventory)
        {
            Debug.Log($"- {item.Name}: {item.Quantity} ");
        }
        Debug.Log("-----------------------------");
    }
}
