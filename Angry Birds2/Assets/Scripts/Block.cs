using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float health = 100f;
    public float speedThreshold = 2f;
    public float rotationThreshold = 10f;
    private bool isBroken = false;

    public Rigidbody2D rigidbody2d;
    public ParticleSystem particles;
    
    public BlockTypes blockType;
    
    public bool isMoving = false;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        
        GameManager.instance.RegisterBlock(this);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 선형 이동과 각회전을 둘다 신경 써야한다.
        // 모든 블록은 Block 스크립트롤 가지고 있다. 계산이 2번 될 수 있다.
        // 서로에게 대미지를 준다?
        // 각자 받는 대미지를 계산한다?

        
        Rigidbody2D otherRigidbody = collision.rigidbody;

        // 나의 선형 속도
        float myLinearVelocity = rigidbody2d.velocity.magnitude;
        // 나의 회전 속도
        float myAngularVelocity = Mathf.Abs(rigidbody2d.angularVelocity);
        
        // 상대의 선형 속도
        float otherLinearVelocity = otherRigidbody != null ? otherRigidbody.velocity.magnitude : 0f;
        // 상대의 회전 속도
        float otherAngularVelocity = otherRigidbody != null ? Mathf.Abs(otherRigidbody.angularVelocity) : 0f;

        // 서로의 속도를 더한다.
        float totalLinearVelocity = myLinearVelocity + otherLinearVelocity;
        float totalAngularVelocity = myAngularVelocity + otherAngularVelocity;

        // 더한 값이 대미지 계산 최소 값보다 클 경우만 작동하게한다. 
        if (totalLinearVelocity > speedThreshold || totalAngularVelocity > rotationThreshold)
        {
            
            float totalImpactVelocity = totalLinearVelocity + (totalAngularVelocity * Mathf.Deg2Rad * 0.1f);

            // 데미지 적용
            float damage = totalImpactVelocity;
            TakeDamage(collision.contacts[0].point, damage);

            
            // 자신과 상대에게 동시에 대미지를 주기 때문에 주의 해야한다.
            Block otherBlock = collision.collider.GetComponent<Block>();
            if (otherBlock != null)
            {
                otherBlock.TakeDamage(collision.contacts[0].point, damage);
            }
        }
    }

    public void TakeDamage(Vector3 position, float damage)
    {
        if (isBroken) return;
        
        health -= damage;
        TextManager.Instance.ShowDamageText(position,damage);
        
        AudioManager.instance.PlaySfxBlock(blockType);

        if (health <= 0)
        {
            DestroyBlock();
            isBroken = true;
        }
    }

    private void DestroyBlock()
    {
        if (!particles.isPlaying)
            particles.Play();
        
        GameManager.instance.UnregisterBlock(this);
        
        Destroy(gameObject, 1f);
    }

    private void FixedUpdate()
    {
        isMoving = rigidbody2d.velocity.magnitude > 0.1f;
    }
}
