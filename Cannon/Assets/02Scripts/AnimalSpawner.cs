using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animals;

    // 소환 범위 설정
    public Vector3 minPosition = new Vector3(-42f, 5f, -70f);
    public Vector3 maxPosition = new Vector3(22f, 30f, 2f);

    IEnumerator Start()
    {
        while (true)
        {
            // 랜덤 대기 시간
            yield return new WaitForSeconds(Random.Range(1f, 5f));
            
            // 랜덤 위치 생성
            float randomX = Random.Range(minPosition.x, maxPosition.x);
            float randomY = Random.Range(minPosition.y, maxPosition.y);
            float randomZ = Random.Range(minPosition.z, maxPosition.z);
            Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
            
            // 랜덤 위치에 동물 소환
            Instantiate(animals[Random.Range(0, animals.Length)], randomPosition, Quaternion.identity);
        }
    }
}