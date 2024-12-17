using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    public float Speed = 5.0f;
    public int switchCount = 0;
    private int moveCount = 0;

    public Vector2 _direction;

    private LayerMask playerlayerMask;


    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        playerlayerMask = LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"JM DEBUG OnTriggerEnter2D {other} {other.gameObject.layer == playerlayerMask}");

        if (other.gameObject.layer == playerlayerMask)
        {
            Rigidbody2D rb = other.GetComponent<hitCollision>().parentRigidbody;

            Vector3 backPosition = rb.transform.position - transform.position;
            backPosition.Normalize();
            backPosition.x *= 3;
            rb.AddForce(backPosition * 800, ForceMode2D.Force);
        }
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(_direction.x * Speed * Time.deltaTime, 0, 0);

        moveCount++;

        if (moveCount >= switchCount)
        {
            _direction *= -1;
            _spriteRenderer.flipX = _direction.x < 0;
            moveCount = 0;
        }
    }
}
