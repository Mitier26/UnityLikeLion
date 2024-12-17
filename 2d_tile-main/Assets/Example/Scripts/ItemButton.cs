using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
            amount.text = itemInfo.amount.ToString();
            amount.gameObject.SetActive(true);
        }  
    }
    
    [SerializeField]Image itemImage;
    [SerializeField] TMP_Text amount;

    void SetItemImage(Sprite sprite)
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
            color.a = 1.0f;
            itemImage.color = color;
        }
    }

    private void Update()
    {
        if (itemInfo != null)
        {
            if (itemInfo.amount != 0)
            {
                amount.gameObject.SetActive(true);
                amount.text = itemInfo.amount.ToString();
            }
        }

    }

}
