using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Inventory : MonoBehaviour
{
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    private ItemButton[] itemButtons;

    private int selectedItemIndex1 = -1;
    private int selectedItemIndex2 = -1;
    
    private void Awake()
    {
        itemButtons = gridLayoutGroup.GetComponentsInChildren<ItemButton>();
        
        ItemManager itemManager = FindObjectOfType<ItemManager>();

        for (var i = 0; i < itemButtons.Length; i++)
        {
            var itemData = itemManager.itemDatas[Random.Range(0, itemManager.itemDatas.Count)];
            
            var i1 = i;
            itemButtons[i].GetComponent<Button>().onClick.AddListener(()=> OnClickItemButton(i1));

            itemButtons[i].GetComponent<ItemButton>().ItemInfo = new ItemInfo()
            {
                amount = 1,
                itemData = itemData
            };
        }
    }

    void OnClickItemButton(int itemIndex)
    {
        if (0 > selectedItemIndex1)
        {
            selectedItemIndex1 = itemIndex;
        }
        else if (0 > selectedItemIndex2)
        {
            selectedItemIndex2 = itemIndex;
            
            var itemInfo1 = itemButtons[selectedItemIndex1].ItemInfo;
            var itemInfo2 = itemButtons[selectedItemIndex2].ItemInfo;
            
            itemButtons[selectedItemIndex1].ItemInfo = itemInfo2;
            itemButtons[selectedItemIndex2].ItemInfo = itemInfo1;
            
            selectedItemIndex1 = -1;
            selectedItemIndex2 = -1;
        }
    }
}
