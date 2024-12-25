using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BirdPool : MonoBehaviour
{
    // 새의 프리팹, 1개 뿐이다.
    public GameObject birdPrefab;
    
    // 새의 정보를 가지고 있는 것, 새의 종류와 연관되어 있다.
    public BirdData[] birdDatas;
    
    // 새 종류 당 만들 새의 수
    public int birdCount = 3;
    
    // 만들어진 새를 담고 있을 모임장
    private List<Bird> birdPool = new List<Bird>();
    
    // 외부에서 풀의 크기가 필요해서 만든 것
    public int poolSize;

    private void Start()
    {
        // Start 에 만들었지만 여기 있으면 에러 생길 것 같고,
        // 다른 필요한 타이밍에 사용하기 위해 함수로 변경
        InitBirdPool();
    }

    private void InitBirdPool()
    {
        for (int i = 0; i < birdDatas.Length; i++)
        {
            for (int j = 0; j < birdCount; j++)
            {
                // 새를 만들고 자식으로 만든다.
                GameObject birdObj = Instantiate(birdPrefab, transform);
                
                Bird bird = birdObj.GetComponent<Bird>();
                
                // 새 내부에 있는 새 초기화
                bird.InitBirdData(birdDatas[i]);
                birdObj.SetActive(false);
                
                birdPool.Add(bird);
                
            }
        }
        
        poolSize =  birdPool.Count;
    }

    public Bird GetBird(int index)
    {
        if (!birdPool[index].gameObject.activeInHierarchy)
        {
            birdPool[index].gameObject.SetActive(true);
            return birdPool[index];
        }
        return null;
    }

    public void ReturnBird(Bird bird)
    {
        bird.gameObject.SetActive(false); 
    }
}

