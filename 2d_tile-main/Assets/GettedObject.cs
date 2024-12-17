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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

