using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEvent : MonoBehaviour
{
    private float timer = 0f;
    public float wantTime = 10f; // ���ϴ� �ð�

    void Update()
    {
        timer += Time.deltaTime; // Ÿ�̸� ���

        if (timer >= wantTime)
        {
            Debug.Log("Ÿ�̸� �˶�");
        }
    }
}
