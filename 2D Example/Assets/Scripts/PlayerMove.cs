using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private CapsuleCollider2D capCollider;

    public float maxSpeed;
    public float jumpPower;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rb.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
        
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * 0.5f, rb.velocity.y);
        }

        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        
        rb.AddForce(Vector2.right * h , ForceMode2D.Impulse);

        if (rb.velocity.x >= maxSpeed)
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        else if (rb.velocity.x < maxSpeed * (-1))
            rb.velocity = new Vector2(maxSpeed * (-1), rb.velocity.y);

        if (rb.velocity.y < 0)
        {
            Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (hit.collider != null)
            {
                if(hit.distance < 0.5f)
                    // Debug.Log(hit.collider.name);
                    anim.SetBool("isJumping", false);
            }
        }
    }

    private void LateUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) <= 0.3f)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            bool isBronze = other.gameObject.name.Contains("Bronze");
            bool isSilver = other.gameObject.name.Contains("Silver");
            bool isGold = other.gameObject.name.Contains("Gold");

            if (isBronze) gameManager.stagePoint += 50;
            else if (isSilver) gameManager.stagePoint += 100;
            else if (isGold) gameManager.stagePoint += 150;
            
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Finish"))
        {
            gameManager.NextStage();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (rb.velocity.y < 0 && transform.position.y > other.transform.position.y)
            {
                OnAttack(other.transform);
            }
            else
            {
                OnDamaged(other.transform.position);
            }
        }
    }

    void OnAttack(Transform enemy)
    {
        gameManager.stagePoint += 100;
        
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    void OnDamaged(Vector2 position)
    {
        gameManager.HealthDown();
        
        gameObject.layer = 9;

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dir = transform.position.x - position.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dir,1)*7, ForceMode2D.Impulse);
        
        anim.SetTrigger("Damaged");
        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        capCollider.enabled = false;
        
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }

    public void VelocityZero()
    {
        rb.velocity = Vector2.zero;
    }
}
