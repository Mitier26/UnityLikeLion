using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    private NavMeshAgent agent;  // NavMeshAgent 컴포넌트를 저장할 변수
    public Transform target;     // 타겟(플레이어 등)을 지정할 변수
    public bool isOnGround = false;  // 바닥에 있는지 여부를 추적하는 변수

    // Start는 초기화 시 한 번 실행
    private void Start()
    {
        // NavMeshAgent 컴포넌트를 가져옵니다.
        agent = GetComponent<NavMeshAgent>();
    
        // 타겟을 지정합니다. (대개 플레이어나 다른 객체)
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update는 매 프레임마다 실행
    private void Update()
    {
        // 바닥에 있을 때만 에이전트가 동작하게 합니다.
        if (target != null)
        {
            // NavMeshAgent의 목적지를 타겟으로 설정하여 계속 따라가게 합니다.
            agent.SetDestination(target.position);
        }
    }

}