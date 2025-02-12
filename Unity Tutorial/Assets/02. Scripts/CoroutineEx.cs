using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineEx : MonoBehaviour
{
    public int endTimer = 10;
    private bool isCancel = false;

    void Start()
    {
        StartCoroutine(BombTimer(10));
    }

    IEnumerator BombTimer(int t)
    {
        int i = t;
        while (i > 0)
        {
            Debug.Log(i + "초 남았습니다.");
            yield return new WaitForSeconds(1f);
            i--;

            if (isCancel)
            {
                Debug.Log("폭탄이 해제됐습니다.");
                yield break;
            }
        }

        Debug.Log("폭탄이 터졌습니다.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCancel = true;
        }
    }
}