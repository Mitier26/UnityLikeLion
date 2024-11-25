using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ComponetStudy : MonoBehaviour
{
    public string nameString = "넌 비어있는 게임오브젝트";

    // public 유니티 인스펙터에서 보인다.
    // 무엇을 목표로 할 것인가?
    public GameObject targetObj;
    // 목표의 컴포넌트에 접근하기!!

    private void Start()
    {
        // Debug.Log : 콘솔창에 출력하는 기능
        Debug.Log(gameObject.name);
        // 이 스크립트가 들어 가있는 현재 gameObject의 이름을 출력해라.
        // this.gameObject : 위에는 this가 생략 되어 있다.
        gameObject.name = nameString;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.AddComponent<Animation>();
        gameObject.AddComponent<BoxCollider>();
        // 컴포넌트를 추가 할 수 있다.
        // 시리얼라이즈 같은 것으로 강제로 컴포넌트를 추가하는 것이 있다고 알 고 있다.
        // 한번 만들어진 컴포넌트는 계속 남아 있다.

        GetComponent<Rigidbody>().useGravity = false;

        // 대상의 컴포넌트 수정하기
        targetObj.name = "목표 게임오브젝트 이름";
        // 대상 오브젝트의 메쉬를 끈다.
        targetObj.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        //targetObj.GetComponent<MeshRenderer>().enabled = false;
        // 대상을 넣어 주어야 한다.
        // 직접 대상을 넣을 수도 있지만 하이라키에 있는 오브젝트를 선택하는 방법도 있다.
        // 아마 오브젝트를 찾는 방법이 여러가지 있을 것이다.
        // tag를 이용하는 방법, 이름을 이용하는 방법 등
        targetObj.GetComponent<CapsuleCollider>().enabled = false;
    }


}
