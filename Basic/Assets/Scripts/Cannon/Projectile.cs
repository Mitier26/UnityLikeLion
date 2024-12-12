using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    public GameObject piecesPrefab; // 파편 프리팹
    public int minPiecesCount = 10; // 최소 파편 개수
    public int maxPiecesCount = 20; // 최대 파편 개수
    public float scatterForce = 10f; // 파편 흩어지는 힘

    private void OnTriggerEnter(Collider other)
    {
        piece2();
    }

    void piece1()
    {
        // 총알 속도 방향 계산
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 bulletDirection = rb.velocity.normalized; // 총알의 날아가던 방향

        // 파편 개수 결정
        int count = Random.Range(minPiecesCount, maxPiecesCount + 1);

        for (int i = 0; i < count; i++)
        {
            // 파편 생성
            GameObject instance = Instantiate(
                piecesPrefab,
                transform.position,
                Random.rotation // 무작위 회전
            );

            // 파편에 힘 적용
            Vector3 randomDirection = Random.insideUnitSphere.normalized; // 무작위 방향
            Vector3 finalDirection = (bulletDirection + randomDirection * 0.5f).normalized; // 총알 방향 + 무작위 약간 흩어짐

            Rigidbody pieceRb = instance.GetComponent<Rigidbody>();
            if (pieceRb != null)
            {
                pieceRb.AddForce(finalDirection * scatterForce, ForceMode.Impulse); // 파편에 힘 추가
            }
        }

        Destroy(gameObject); // 총알 파괴
    }

    void piece2()
    {
        Vector3 hitDirection = GetComponent<Rigidbody>().velocity * -1;

        int count = Random.Range(minPiecesCount, maxPiecesCount + 1);

        for (int i = 0; i < count; ++i)
        {
            Vector3 randomDiretion = Random.insideUnitSphere;
            Vector3 lastDiretion = Quaternion.LookRotation(randomDiretion) 
                                   * hitDirection;
        
            GameObject instance = Instantiate(piecesPrefab, transform.position,
                Quaternion.LookRotation(lastDiretion));

            instance.GetComponent<Rigidbody>().AddForce(lastDiretion, ForceMode.Impulse);
            Destroy(this.gameObject);
        }
    }
}