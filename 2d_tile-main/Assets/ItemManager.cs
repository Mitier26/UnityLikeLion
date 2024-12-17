using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo
{
    public ItemData itemData;
    public int amount;
}

public class ItemManager : MonoBehaviour
{
    public List<ItemData> itemDatas = new List<ItemData>();
}
