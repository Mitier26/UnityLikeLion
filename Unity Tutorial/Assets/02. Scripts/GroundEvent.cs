using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEvent : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 여기까지 통과한 대상은? -> Player Tag를 가진 오브젝트
            GameObject characterObj = collision.gameObject;

            // 캐릭터 오브젝트의 Movement 스크립트(컴포넌트)에 접근해서 isGround를 변경 필요!
            characterObj.GetComponent<Movement>().isGround = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameObject characterObj = collision.gameObject;

            characterObj.GetComponent<Movement>().isGround = false;
        }
    }
}