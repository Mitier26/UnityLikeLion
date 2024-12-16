using System;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite OnHeart;
    public Sprite OffHeart;

    public SpriteRenderer SpriteRenderer;
    public int LiveNumber;

    private void Update()
    {
        if (GameManager.Instance.lives >= LiveNumber)
        {
            SpriteRenderer.sprite = OnHeart;
        }
        else
        {
            SpriteRenderer.sprite = OffHeart;
        }
    }
}
