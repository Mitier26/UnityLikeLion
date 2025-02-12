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
            Debug.Log(i + "�� ���ҽ��ϴ�.");
            yield return new WaitForSeconds(1f);
            i--;

            if (isCancel)
            {
                Debug.Log("��ź�� �����ƽ��ϴ�.");
                yield break;
            }
        }

        Debug.Log("��ź�� �������ϴ�.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCancel = true;
        }
    }
}