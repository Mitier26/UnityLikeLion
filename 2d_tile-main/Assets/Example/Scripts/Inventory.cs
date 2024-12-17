using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    private ItemButton[] buttons;

    private int selectedItemIndex1 = -1;
    private int selectedItemIndex2 = -1;

    // Start is called before the first frame update
    void Awake()
    {
        buttons = gridLayoutGroup.
            GetComponentsInChildren<ItemButton>();

        // ItemManager itemManager = FindObjectOfType<ItemManager>();
        for (var i = 0; i < buttons.Length; i++)
        {
            var i1 = i;
            buttons[i].GetComponent<Button>().
                onClick.AddListener(() =>
                    OnClickItemButton(i1)
                    );
        }
    }

    public void AddItem(GettedObject item)
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            

            if (buttons[i].ItemInfo != null)
            {
                if (buttons[i].ItemInfo.itemData == item.ItemData)
                {
                    buttons[i].ItemInfo.amount += 1;

                    break;
                }

            }
            else if (buttons[i].ItemInfo == null)
            {
                buttons[i].ItemInfo = new ItemInfo() { itemData = item.ItemData, amount = 1 };
                break;
            }
        }

        // buttons[i].GetComponent<ItemButton>().ItemInfo = new ItemInfo()
        // {
        //     amount = 1,
        //     itemData = itemData
        // };
    }

    void OnClickItemButton(int index)
    {
        if (0 > selectedItemIndex1)
        {
            selectedItemIndex1 = index;
        }
        else if (0 > selectedItemIndex2)
        {
            selectedItemIndex2 = index;

            var itemInfo1 = buttons[selectedItemIndex1].ItemInfo;
            var itemInfo2 = buttons[selectedItemIndex2].ItemInfo;
            buttons[selectedItemIndex1].ItemInfo = itemInfo2;
            buttons[selectedItemIndex2].ItemInfo = itemInfo1;
            selectedItemIndex1 = -1;
            selectedItemIndex2 = -1;
        }
    }

}
