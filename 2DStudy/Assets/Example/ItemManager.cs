using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Datas/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
}

public class ItemInfo
{
    public ItemData itemData;
    public int amount;
}
public class ItemManager : MonoBehaviour
{
    public List<ItemData> itemDatas = new List<ItemData>();
}
