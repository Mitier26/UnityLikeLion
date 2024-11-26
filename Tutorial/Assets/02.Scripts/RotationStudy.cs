using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationStudy : MonoBehaviour
{
    public float rot = 30f;
    public float rotSpeed = 10f;
    public float moveSpeed = 10f;

    public Transform target;

    float h = 0f, v = 0f;

    private void Start()
    {
        //ResetTransform();
        //AngleStudy();

    }

    private void Update()
    {
        // 자기 자신이 회전
        // SelfRotation();

        // 특정 오브젝트를 회전
        // target.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);

        // 특정 오브젝트 주변을 회전
        // TargetAround();

        // 캐릭터가 이동하려는 방향을 바라보게 하는 코드
        LookAtStudy();

    }

    void ResetTransform()
    {
        // 자기 자신의 위치를 ( 0, 0, 0 ) 위치로 변경
        transform.position = Vector3.zero;

        // 자기 자신의 회전을 ( 0, 0, 0 ) 으로 변경
        transform.rotation = Quaternion.identity;
        // 월드 기준 좌표, 월드 기준의 정면을 바라보는 회전.

        // 자기 자신의 크기를 ( 1, 1, 1 ) 로 변경
        transform.localScale = Vector3.one;
    }
    void AngleStudy()
    {
        // 오일러 회전을 작성 하고 쿼터니얼 회전으로 변환

        // 원하는 각도로 변경
        Vector3 newRotation = new Vector3(0, rot, 0f);

        // transform.rotation = newRotation;
        // 이것 에러
        // transform.rotation은 Quaternion 타입, newRotation는 Vector3 타입
        // 서로의 타입이 다르기 때문에 넣는 것은 불가능

        // Vector3를 Quaternion으로 변화하는 것을 찾아야 한다.
        transform.rotation = Quaternion.Euler(newRotation);
        // Quaternion.Euler
        // Euler 각도를 Quaternion 각도로 변경

        Debug.Log(transform.rotation);

        // 쿼터니언에서 오일러로 변환
        Debug.Log(transform.rotation.eulerAngles);
    }

    void SelfRotation()
    {
        // 계속 회전
        // Y축을 기준으로 회전
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        // Rotate : 회전하다
        // 회전하다, 자신위 위 기준 * 속도 * 프레임 보간
    }

    void TargetAround()
    {
        transform.RotateAround(target.position, Vector3.up, rotSpeed * Time.deltaTime);
    }

    void LookAtStudy()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // 이동 방향
        Vector3 dir = new Vector3(h, 0, v).normalized;

        transform.position += dir * moveSpeed * Time.deltaTime;

        // 키를 눌렀을 때만 작동
        if (h != 0 || v != 0)
        {
            // 목표의 위치
            Vector3 tarPos = transform.position + dir;
            // 자시 자신의 위치 기준 + 방향 = 가려고 하는 위치

            // 목표 위치를 바라보는 코드
            transform.LookAt(tarPos);
        }
    }
}
