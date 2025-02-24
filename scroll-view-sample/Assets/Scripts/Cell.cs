using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text subtitleText;

    public void SetItem(Item item)
    {
        image.sprite = Resources.Load<Sprite>("lion");
        titleText.text = item.title;
        subtitleText.text = item.subtitle;
    }
    
}
