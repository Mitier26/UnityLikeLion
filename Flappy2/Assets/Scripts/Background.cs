using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private SpriteRenderer sr;
    public float offsetSpeed = 1f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float offsetVal = offsetSpeed * Time.deltaTime;
        sr.material.SetTextureOffset("_MainTex", sr.material.mainTextureOffset + new Vector2(offsetVal, 0f ));
    }
}
