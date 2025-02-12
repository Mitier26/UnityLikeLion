using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ExampleClass : MonoBehaviour
{
    public int[] array1 = { 1, 2, 3, 4, 5 }; // int 타입의 배열 선언하고 5개 값을 할당 -> 5개 공간 생성
    public float[] array2; // float 타입의 배열 선언
    public GameObject[] array3 = new GameObject[3]; // GameObject 타입의 배열 선언 & 3개 공간 생성

    public List<int> array4 = new List<int> { 1, 2, 3, 4, 5 };
    public List<float> array5;
    public List<GameObject> array6 = new List<GameObject>();
}