using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    // 안녕! 나는 주석이야. 한글이 잘 되는지 테스트 해봤어.

    private void Awake()
    {
        Debug.Log("Awake 함수");
    }
    private void Start()
    {
        Debug.Log("Start 함수");
    }

    private void Update()
    {
        //Debug.Log("Update 함수");
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate 함수");
    }

    private void LateUpdate()
    {
        Debug.Log("LateUpdate 함수");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable 함수");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable 함수");
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy 함수");
    }

}
