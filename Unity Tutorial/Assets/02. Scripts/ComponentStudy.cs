using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentStudy : MonoBehaviour
{
    public Transform destination; // 목적지 오브젝트

    void Start()
    {
        // 자기 자신의 Transform의 위치를       destination의 위치로 할당한다.
        // this.gameObject.transform.position = destination.position;
        transform.position = destination.position;
    }
}