using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public struct DamageFieldData
{
    public float distance;
    // 구조체, 지금은 공격의 범위만 가지고 있다.
}

public class CharController : MonoBehaviour
{
    private const float jumpTestValue = 0.3f;
    private static readonly int Speed1 = Animator.StringToHash("Speed");
    private static readonly int Ground = Animator.StringToHash("Ground");
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private float JumpSpeed = 15.0f;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float CameraSpeed = 4.0f;
    [SerializeField] private float MaxDistence = 4.0f;

    private Vector3 cameraOffset;

    InputAction Move_Input;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private InputAction Jump_Input;

    public List<CButton> _buttons;                      // 화면에 표시되는 버튼들
    
    public List<DamageField> _damageFields;             // 생성되는 게임 오브젝트의 스크립트
    public List<DamageFieldData> _damageFieldDatas;     // 공격의 거리, 위에 있는 구조체

    [NonSerialized] public int Grounded = 0;
    
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        UnityEngine.InputSystem.PlayerInput Input = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        Move_Input = Input.actions["Move"];
        Jump_Input = Input.actions["Jump"];

        cameraOffset = _mainCamera.transform.position - transform.position;
        
        // 버튼을 초기화하는 것, 버튼 리스트를 돌아 스킬을 대입했다.
        // 스킬이 여러개 이면 어떻게 할 것인가?
        // 이렇게 하면 모든 버튼이 같은 스킬을 가지게 될 것이다.
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].Index = i;
            _buttons[i].AddListener(FireSkill);
        }
        
        
    }

    bool canMove = true;
    private int currentButton = -1;

    void FireDamageField()
    {
        Debug.Log(currentButton);
        GameObject go = Instantiate(_damageFields[currentButton].gameObject);
        go.GetComponent<DamageField>().MyOwnerTag = gameObject.tag;
        go.transform.position = transform.position + transform.right * _damageFieldDatas[currentButton].distance;
        Destroy(go, 3.0f);
    }

    void CanMove(int bMove)
    {
        canMove = bMove == 1;
    }

    void FireSkill(int skillIndex)
    {
        // StartCoroutine(FireSkillCoroutine());
        _animator.Rebind();
        _animator.Play("Attack");
        currentButton = skillIndex;
        
    }


    IEnumerator FireSkillCoroutine()
    {
        _animator.Rebind();
        _animator.Play("Attack");
        yield return null;
        var curState = _animator.GetCurrentAnimatorStateInfo(0);
        while (1.0 > curState.normalizedTime)
        {
            yield return null;
        }
    }

    void FixedUpdate()
    {
        Vector2 moveValue = Move_Input.ReadValue<Vector2>();

        if (!canMove)
        {
            moveValue = Vector2.zero;
        }

        if (moveValue.x != 0)
            _spriteRenderer.flipX = moveValue.x < 0;

        _animator.SetFloat(Speed1, Mathf.Abs(moveValue.x));

        if (moveValue.x == 0)
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);

        transform.position += new Vector3(moveValue.x * Speed * Time.deltaTime, 0, 0);

    }

    void Update()
    {
        if (Jump_Input.triggered && Grounded >= 1)
        {
            _rigidbody.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);
            _animator.Play("Alchemist_Jump");
        }
    }

    private void LateUpdate()
    {
        var CharPosition = transform.position + cameraOffset;
        float speed = CameraSpeed;

        Vector3 newPosition = Vector3.zero;

        if (Vector3.Distance(CharPosition, _mainCamera.transform.position) >= MaxDistence)
        {
            Vector3 Gap = ((_mainCamera.transform.position) - CharPosition).normalized * MaxDistence;
            newPosition = CharPosition + Gap;
        }
        else
        {
            newPosition = Vector3.MoveTowards(_mainCamera.transform.position,
                CharPosition,
                speed * Time.deltaTime);
        }

        _mainCamera.transform.position = newPosition;
    }
}
