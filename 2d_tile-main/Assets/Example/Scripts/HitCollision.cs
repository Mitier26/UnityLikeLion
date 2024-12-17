using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitCollision : MonoBehaviour
{
    [NonSerialized] public Rigidbody2D parentRigidbody;
    [SerializeField] private float decelerationRate = 5f;
    [SerializeField] private float minVelocity = 0.1f;

    private Vector3 knockbackVelocity;

    void Start()
    {
        parentRigidbody = GetComponentInParent<Rigidbody2D>();
    }


    public void Knockback(Vector2 direction, float power)
    {
        StartCoroutine(KnockbackCoroutine(direction, power));
    }

    IEnumerator KnockbackCoroutine(Vector2 direction, float power)
    {
        knockbackVelocity = direction.normalized * power;

        while (true)
        {
            if (knockbackVelocity.magnitude < minVelocity)
            {
                knockbackVelocity = Vector3.zero;
                yield break;
            }

            // 지수적 감속 적용 (점점 더 천천히 감속)
            knockbackVelocity *= (1f - Time.deltaTime * decelerationRate);

            // Transform으로 이동 적용
            parentRigidbody.transform.position += knockbackVelocity * Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }
    }
}
