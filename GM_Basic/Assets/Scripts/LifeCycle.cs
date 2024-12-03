using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("플레이어 데이터가 준비되었다");
    }

    private void OnEnable()
    {
        Debug.Log("플레이어 로그인");
    }

    private void Start()
    {
        Debug.Log("사냥 장비를 챙겼다");
    }

    private void FixedUpdate()
    {
        Debug.Log("이동");
        // 설정된 프레임으로 실행된다.
    }

    private void Update()
    {
        Debug.Log("몬스터 사냥");
        // 프레임과는 상관없이 컴퓨터 성능에 따라 실행
    }

    private void LateUpdate()
    {
        Debug.Log("경험치 획득");
        // 모든 Update가 끝나고 실행, 후처리 할 때 사용
    }

    private void OnDisable()
    {
        Debug.Log("플레이어 로그 아웃");
    }

    private void OnDestroy()
    {
        Debug.Log("플레이어 데이터를 삭제 했다");
        // 오브젝트가 삭제되기 전에 무엇인가 남기고 삭제
    }
}
