using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")] public float JumpForce;
    [Header("Reference")]
    public Rigidbody2D PlayerRigidbody2d;

    public Animator PlayerAnimator;

    public BoxCollider2D PlayerCollider;

    private bool isGrounded = true;

    public bool isInvincible = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlayerRigidbody2d.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("state", 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Platform")
        {
            if (!isGrounded)
            {
                PlayerAnimator.SetInteger("state", 2);
            }
            isGrounded = true;
        }
    }

    public void KillPlayer()
    {
        PlayerCollider.enabled = false;
        PlayerAnimator.enabled = false;
        PlayerRigidbody2d.AddForceY(JumpForce, ForceMode2D.Impulse);
    }
    
    void Hit()
    {
        GameManager.Instance.lives -= 1;
    }

    void Heal()
    {
        GameManager.Instance.lives = Mathf.Min(3, GameManager.Instance.lives + 1);
    }

    void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible",5f);
    }

    void StopInvincible()
    {
        isInvincible = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!isInvincible)
            {
                Destroy(other.gameObject);
                Hit();
            }
        }
        else if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            Heal();
        }
        else if (other.CompareTag("Golden"))
        {
            Destroy(other.gameObject);
            StartInvincible();
        }
    }
}
