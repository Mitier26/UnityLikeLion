using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    public Slider _slider;

    public int maxHp = 5;
    private int hp = 5;

    public int Hp
    {
        get { return hp; }
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
            
            _slider.value = (float)hp / maxHp;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    
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
        _slider = GetComponentInChildren<Slider>();
        _slider.maxValue = maxHp;
        _slider.value = hp;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"JM DEBUG OnTriggerEnter2D {other} {other.gameObject.layer == playerlayerMask}");

        if (other.gameObject.layer == playerlayerMask)
        {
            Rigidbody2D rb = other.GetComponent<HitCollision>().parentRigidbody;

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
