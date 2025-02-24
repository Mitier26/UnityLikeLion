using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataClass2 : EventArgs
{
    public string dataName;
    
    public DataClass2(string dataName)
    {
        this.dataName = dataName;
    }
}

public class ExampleEventHandler : MonoBehaviour
{
    private int number;
    public int Number { get => number; private set => number = value; }

    private EventHandler startHandler;

    public event EventHandler StartHandler
    {
        add
        {
            Debug.Log($"{value.Method} 함수 추가");
            startHandler += value;
        }
        remove
        {
            Debug.Log($"{value.Method} 함수 제거");
            startHandler -= value;
        }
    }
    
    private void OnEnable()
    {
        // 구독
        DataClass2 data = new DataClass2("New Data Name");
        StartHandler += StartEvent;

        startHandler(this, data);
    }

    private void OnDisable()
    {
        // 구독 해제
        StartHandler -= StartEvent;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            startHandler?.Invoke(this, EventArgs.Empty);
        }
    }

    // 알림 이벤트
    public void StartEvent(object o, EventArgs e)
    {
        Debug.Log("Start Event");
        Debug.Log(((DataClass2) e).dataName);
        DataClass2 data = (DataClass2)e;
        Debug.Log(data.dataName);
    }
}
