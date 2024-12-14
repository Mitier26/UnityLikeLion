using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;
    float h;
    float v;
    private bool isHorizonMove;
    
    Rigidbody2D rigid;
    private Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");
        
        if(hDown) isHorizonMove = true;
        else if(vDown) isHorizonMove = false;
        else if (hUp || vUp) isHorizonMove = h != 0;

        if (anim.GetInteger("hAxis") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxis", (int)h);
        }
        else if (anim.GetInteger("vAxis") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxis", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveVew = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVew * speed;
    }
}
