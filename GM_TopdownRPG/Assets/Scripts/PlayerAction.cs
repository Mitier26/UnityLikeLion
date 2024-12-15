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
    private Vector3 direction;
    public GameObject scanObject;

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

        if (vDown && v == 1)
            direction = Vector3.up;
        else if (vDown && v == -1)
            direction = Vector3.down;
        else if (hDown && h == -1)
            direction = Vector3.left;
        else if (hDown && h == 1)
            direction = Vector3.right;

        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            Debug.Log(scanObject.name);
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveVew = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVew * speed;
        
        Debug.DrawRay(rigid.position, direction * 0.7f, new Color(0, 1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, direction , 0.7f, LayerMask.GetMask("Object"));
        
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
}
