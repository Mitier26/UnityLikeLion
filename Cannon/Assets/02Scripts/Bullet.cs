using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject piecesPrefab; // 파편 프리팹
    public int minPiecesCount = 10; // 최소 파편 개수
    public int maxPiecesCount = 20; // 최대 파편 개수
    public float scatterForce = 10f; // 파편 흩어지는 힘
    
    private void OnTriggerEnter(Collider other)
    {
        piece2(other);
    }
    
    void piece2(Collider other)
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
        
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
