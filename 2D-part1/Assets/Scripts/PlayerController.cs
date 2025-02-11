using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    public float moveSpeed = 5f; // 이동 속도를 조정할 수 있는 변수

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake() 
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // WASD 입력값 가져오기
        float horizontal = Input.GetAxis("Horizontal"); // A, D 또는 화살표 좌우
        float vertical = Input.GetAxis("Vertical"); // W, S 또는 화살표 위/아래

        // 이동 방향 계산
        Vector3 moveDirection = new Vector3(horizontal, vertical, 0f);

        // 대각선 이동 속도 균등화
        moveDirection = moveDirection.normalized;

        // 캐릭터 이동
        transform.Translate(moveDirection * (moveSpeed * Time.deltaTime), Space.World);
            
        // Sprite 방향 처리
        if (horizontal < 0) 
        {
            // 왼쪽으로 이동 중
            _spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            // 오른쪽으로 이동 중
            _spriteRenderer.flipX = false;
        }
            
            
        // Animator에 이동 속도를 전달
        _animator.SetFloat(Speed, moveDirection.magnitude);
    }
}
