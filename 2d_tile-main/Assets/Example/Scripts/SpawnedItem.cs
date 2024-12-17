using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedItem : MonoBehaviour
{
    public Action OnDestroiedAction;
    public ItemData itemData;

    public void SetItemData(ItemData itemData)
    {
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        this.itemData = itemData;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        OnDestroiedAction?.Invoke();
    }
}
