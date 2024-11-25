using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformStudy : MonoBehaviour
{
    public Transform destination;  // 목적지!
    // 모든 GameObject는 Transform을 가지고 있기 때문에

    private void Start()
    {
        // 자기 이동 시키기
        // 자기 자신의 Transform에 접근해서 positon 값을 변경
        transform.position = new Vector3(1, 0, 0);
        // 왜 gameObject가 없어도 작동하는 것이니?
        // 모든 Unity Object는 Transform이 필수 이기 때문에


        destination = GameObject.Find("Target").transform;

        transform.position = destination.position;

    }

    private void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, destination.position, 1 * Time.deltaTime);
    }

    private void OnEnable()
    {
        transform.position += Vector3.forward;
    }
}
