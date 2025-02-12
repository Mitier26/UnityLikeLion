using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMap : MonoBehaviour
{
    private SpriteRenderer sp;
    public float offsetSpeed = 0.5f;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float offsetVal = offsetSpeed * Time.deltaTime;
        sp.material.SetTextureOffset("_MainTex", sp.material.mainTextureOffset + new Vector2(offsetVal, 0f));
    }
}