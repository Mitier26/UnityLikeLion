using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEvent : MonoBehaviour
{
    private float timer = 0f;
    public float wantTime = 10f; // 원하는 시간

    void Update()
    {
        timer += Time.deltaTime; // 타이머 기능

        if (timer >= wantTime)
        {
            Debug.Log("타이머 알람");
        }
    }
}
