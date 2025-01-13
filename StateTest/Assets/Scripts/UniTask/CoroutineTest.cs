using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    public int hp = 1;

    private void Start()
    {
        StartCoroutine(RecoveryHp());
    }

    private IEnumerator RecoveryHp()
    {
        Debug.Log("RecoveryHp started");
        yield return new WaitForSeconds(1.0f);
        int count = 0;
        while (count < 10)
        {
            count++;
            hp++;
            Debug.Log(hp);
            yield return new WaitForSeconds(1.0f);
        }
        
    }
}
