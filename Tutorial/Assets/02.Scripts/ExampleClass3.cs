using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleClass3 : MonoBehaviour
{
    // 배열
    // 특정 데이터가 들어가는 공간을 만들면 "추가 불가"!!
    // 배열을 만들 때 공간이 정해진다.
    public int[] array1 = { 1, 2, 3, 4, 5 };
    // int 타입의 공간을 선언하고 5개의 데이터가 들어가 있다. -> 5개 공간 생성
    public float[] array2;
    // float 타입의 데이터를 받겠다고 선언만 한 상태
    public GameObject[] array3 = new GameObject[3];
    // 3개의 GameObject공간만 만든다.
    // 특정 값을 넣지 않으면 null 값이 들어 가 있다.

    public List<int> array4 = new List<int> { 1, 2, 3, 4, 5 };
    public List<float> array5;
    public List<GameObject> array6 = new List<GameObject>();

    // 뒤에 new가 없어도 자동으로 생성해준다.

    // 리스트는 수정이 쉽다. -> 추가, 삽입, 삭제
    // 각 데이터는 서로 떨어져 있고 주소로 연결 되어 있다.
    // 그래서 데이터를 찾는 속도가 상대적으로 느리다.

    // 데이터의 갯수가 정해져 있다면 Array 가 더 빠르다.

    // 배열과 리스트를 언제 사용할 지 선택하는 것이 중요!!
    // 정적 과 동적

    private void Start()
    {

    }
}
