using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleCallback2 : MonoBehaviour
{
    private Animator anim;

    public Action startAction;
    public Action endAction;
    
    private void Start()
    {
        startAction += SwingStart;
        endAction += SwingEnd;
        
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SwingRoutine());
        }
    }
    
    IEnumerator SwingRoutine()
    {
        startAction?.Invoke();
        anim.SetTrigger("Swing");
        
        float animTime = anim.GetCurrentAnimatorStateInfo(0).length;
        float nAnimTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        
        yield return new WaitForSeconds(animTime);
        
        endAction?.Invoke();
    }

    public void SwingStart()
    {
        Debug.Log("Swing Start");
    }

    public void SwingEnd()
    {
        Debug.Log("Swing End");
    }
}
