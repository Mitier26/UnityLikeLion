using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    void FixedUpdate()
    {
        Vector2 moveValue = Move_Input.ReadValue<Vector2>();

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
