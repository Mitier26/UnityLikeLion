using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : BaseInputHandler
{
    CharacterController _controller;

    [NonSerialized] public Action OnInteract;
    [NonSerialized] public Action OnAttack;

    private IEnumerator Start()
    {
        _controller = GetComponent<CharacterController>();
        OnAction += DirectControl;
        _controller.SetInputHandler(this);
        
        // BaseInputHandler의 Update에서 OnAction을 계속 실행하고 있다.
        // Action은 매개변수가 없고 반환값이 없는 함수를 저장할 수 있는 것이다.
        // CharacterInputHandle는 BaseInputHandler를 상속 받았기 때문에 부모에 있는 OnAction을 사용할 수 있다.
        // 부모의 액션에 DirectionControl 함수를 넣은 것
        // 그러먼 부모의 Update에 의 해 DirectControldl 매 프레임 실행되는 것과 같다.
        
        yield return new WaitForSeconds(0.1f);
        _controller.SetInputHandler(this);
    }

    void DirectControl()
    {
        // 여기서 입력에 관한 것을 하고 있다.
        // 하지만 누가 이것을 사용하는 지는 알 수 없다.
        // 이동 방향 입력 처리
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        MovementInput = new Vector2(horizontal, vertical).normalized;
        Horizontal = horizontal;
        Vertical = vertical;

        // 키 입력 상태 갱신
        IsWalking = MovementInput.magnitude > 0.1f;
        IsJumping = IsGrounded && Input.GetKeyDown(KeyCode.Space);
        IsRunning = IsWalking && Input.GetKey(KeyCode.LeftShift);
        
        if(Input.GetMouseButtonDown(0)){
            OnAttack?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.E)){
            OnInteract?.Invoke();
        }
    }
}
