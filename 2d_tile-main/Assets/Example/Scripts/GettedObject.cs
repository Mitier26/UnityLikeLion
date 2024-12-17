using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GettedObject : MonoBehaviour
{
    public ItemData ItemData;

    public void SetItemData(ItemData itemData)
    {
        GetComponent<Image>().sprite = itemData.icon;
        this.ItemData = itemData;
    }
}
