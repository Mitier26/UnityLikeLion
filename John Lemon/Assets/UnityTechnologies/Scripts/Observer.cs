using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Observer : MonoBehaviour
{
    // 플레이어 오즈벡트의 위치
    public Transform player;

    // 플레이어가 시아에 들어왔니?
    private bool m_IsPlayerInRange;

    public GameEnding gameEnding;

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 Collider가 player이면
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (m_IsPlayerInRange)
        {
            // 나의 플레이어 사이의 방향
            // 상대 - 나
            // 마지막에 Vector3.up을 한 이유는 player.position 는 바닥이기 때문!!
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
            }

            if (raycastHit.collider.transform == player)
            {
                gameEnding.CaughtPlayer();
            }
        }
    }
}
