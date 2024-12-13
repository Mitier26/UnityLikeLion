using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    private ItemInfo itemInfo;

    public ItemInfo ItemInfo
    {
        get => itemInfo;
        set
        {
            itemInfo = value;
            SetItemImage(itemInfo.itemData.icon);
        }
    }

    [SerializeField] private Image itemImage;

    public void SetItemImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
        if (sprite == null)
        {
            var color = itemImage.color;
            color.a = 0;
            itemImage.color = color;
        }
        else
        {
            var color = itemImage.color;
            color.a = 1;
            itemImage.color = color;
        }
    }
}
