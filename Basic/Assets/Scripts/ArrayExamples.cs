using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrayExamples : MonoBehaviour
{
    #region Values

        // 배열의 크기
        private const int ARRAY_SIZE  = 5;
        // 플레이어 점수를 저장하는 배열
        private int[] playerScores = new int[ARRAY_SIZE];

        // 아이템 이름을 저장하는 배열
        private string[] itemNames = { "검", "방패", "포션", "활", "마법서" };
        
        // 적 프리팹을 저장하는 배열
        public GameObject[] enemyPrefabs;
        
        // 맵의 타일 타입을 저장하는 2D 배열
        private int[,] mapTiles = new int[10, 10];

        // 1차우너 배열도 같은 메모리 공간을 가진다.
        private int[] mapTiles2 = new int[100];
        
        
        public GameObject cube;
        public GameObject sphere;
        private GameObject[,] cubeMapTiles = new GameObject[10, 10];

    #endregion
    
    
    private void Start()
    {
        // PlayerScoreExample();
        // ItemInventoryExample();
        // EnemySpawnExample();
        MapGenerationExample();
    }

    void PlayerScoreExample()
    {
        // 플레이어 점수 할당
        for (int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i] = Random.Range(100, 1000);
            // 5개의 배열 칸에 100 ~ 1000 사이의 무작위 수를 대입
        }

        int maxValue = 0;           // 최고값을 넣을 변수
        float averageValue1 = 0;    // 평균값을 넣을 변수
        int totalValue = 0;         // 총값을 넣을 변수

        // 반복문, 5번 반복, 이유는 playerScores배열의 길이
        for (int i = 0; i < playerScores.Length; i++)
        {
            // 조건문
            if (playerScores[i] > maxValue)
            {
                // 지금 playerScores 각 칸에는 100 ~ 1000 사이의 무작위 값이 있다
                // 조건문에서
                // 0번의 값이 maxValue 보다 크면 아래를 실행하라고 한다.
                // maxValue의 초기값는 0 이기 때문에 조건에 성립!!!
                // 아래를 실행한다.
                maxValue = playerScores[i];
                // 최고 값은 0 번 값이 되었다.
                // 다시 반복문으로 가 1번 칸에 관한 것을 수행한다.
                // 그리고, 각 칸의 점수가 변경된 maxValue 보다 크면!
                // 여기로 들어와 maxValue를 더 높은 값으로 변경한다.
            }
        }
        
        // 반복문
        for (int i = 0; i < playerScores.Length; i++)
        {
            totalValue += playerScores[i];
            // += 추가
            // totalValue = totalValue + playerScores[i] 와 같은 뜻
            // 값을 더하고 더하고 더한다.
        }
        
        // 총점 출력
        Debug.Log($"총 점은 : {totalValue}");
        
        // 최고 점수 출력
        Debug.Log($"최고 점수 : {maxValue}");
        
        // 최고 점수 찾기, 배열의 기능을 이용하는 방법
        int hightestScore = playerScores.Max();
        Debug.Log($"최고 점수 : {hightestScore}");
        
        // 평균 구하기
        averageValue1  = totalValue / (float)playerScores.Length;
        Debug.Log($"평균 점수1 : {averageValue1:F2}");
        // 배열의 기능을 이용한 평균 구하기
        double averageValue2 = playerScores.Average();
        Debug.Log($"평균 점수2 : {averageValue2:F2}");
        // 기본적으로 지원하는 기능을 잘 사용하면 짧고 편하게 쓸수 있다!
    }

    void ItemInventoryExample()
    {
        // 램덤 아이템 선택
        int randomIndex = Random.Range(0, itemNames.Length);
        // 0 ~ 아이템 길이 사이의 랜덤 값
        string selectedItem = itemNames[randomIndex];
        // private string[] itemNames = { "검", "방패", "포션", "활", "마법서" };
        // 0 번 칸에는 "검", 1 번 칸에는 "방패" 가 있다.
        Debug.Log($"선택된 아이템 : {selectedItem}");
        
        
        // 해당 값이 있는지 확인하는 것
        
        // Array로 Contains 구현
        string itemName = "포션";
        // 함수로 만들 었다. 함수는 아래
        // 포함하고 있는지 확인하는 함수
        bool hasPotion1 = Contains1(itemName);
        Debug.Log($"포션1 보유 했니? {hasPotion1}");
        
        // 특정 아이템 검색
        string searchItem = "포션";
        bool hasPotion2 = itemNames.Contains(searchItem);
        Debug.Log($"포션2 보유 여분 : {hasPotion2}");
        
        // 위 두개의 Debug의 값는 같다.
        // 위에 것은 직접구현한 것, 아래 것은 배열의 기능!
        // Max : 배열중 최고값을 반환하는 기능
        // Average : 배열의 평균값을 반환하는 기능
        // Contains : 배열에 해당 값이 있는지 확인하는 기능
    }

    private bool Contains1(string itemName)
    {
        for (var i = 0; i < itemNames.Length; i++)
        {
            if (itemNames[i] == itemName)
            {
                return true;
            }
        }
        return false;
    }
    
    void EnemySpawnExample()
    {
        if (enemyPrefabs != null && enemyPrefabs.Length > 0)
        {
            // 랜덤 위치에 랜덤 적 생성
            Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            // 소환 지점을 정한다.
            // 새로운 Vector3로  x : -10 ~ 10 까지, y : 0, z : -10 ~ 10까지 수 중에 생선한다.
            // 예 ( 2.3f, 0, 5.2f ) 또는 ( -9.8, 0, 1.2f ) 등등등
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            // 소환할 몬스터를 선택한다.
            // 코드로 배열의 크기를 정하지 않았지만 인스펙터를 이용해 크기를 정할 것이다.
            Instantiate(enemyPrefabs[randomEnemyIndex], spawnPosition, Quaternion.identity);
            // Instantiate : 특정 GameObject를 생성한다.
            // Instantiate ( 생성할 오브젝트, 위치, 회전 )
            // Quaternion.identity : 오브젝트의 기본 회전값
            Debug.Log($"적 생성된 : {enemyPrefabs[randomEnemyIndex].name}");
        }
        else
        {
            Debug.LogWarning("적 프리패밍 할당되지 않았습니다.");
        }
        
    }
    
    void MapGenerationExample()
    {
        // 간단한 맵 생성 ( 0 : 빈 공간, 1 : 벽 )
        for (int x = 0; x < mapTiles.GetLength(0); x++)
        {
            // 2중 for문!!
            // [10, 10] 이라면 
            // [ 0, 0 ~ 9 ] 까지 반복, 내부 for문 반복
            // [ 1, 0 ~ 9 ] 외부 for만 1 올리고 내부 for문 반복
            for (int y = 0; y < mapTiles.GetLength(1); y++)
            {
                // [10, 10] 이라면 총 100 번 반복 한다.
                mapTiles[x, y] = Random.value > 0.8f ? 1 : 0;
                // Random.value : 0 ~ 1 사이의 값
                // 상항 연산자
                // Random.value의 값이 0.8 보다 크면 1 아니면 0
                // if문으로 풀어 작성할 수 있다. 반대로 if문의 간략버전
                // if(Random.value > 0.8f)
                // {
                //      return 1;
                // }
                // else
                // {
                //      return 0;
                // }
                //  if {} 안에 다른 것이 있으면 에러!! return만 있어야 한다.
                
                // 위에 것을 작동하면
                // 0.8의 확률 80% 확률로 1 , 20% 확률로 0이 가로10 세로10 칸 안에 대입되었다.
            }
        }
        
        // 맵 출력
        // 위에서 만든 것이 잘 만들어 졌는지 확인 하는 것
        string mapString = "생성된 맵:\n";      // 문자열 변수를 만든다.
        for (int x = 0; x < mapTiles.GetLength(0); x++)
        {
            for (int y = 0; y < mapTiles.GetLength(1); y++)
            {
                // 100 번 반복해 1 이면 ■  띄어쓰기 있음
                mapString += mapTiles[x, y] == 1 ? "■ " : "□ ";
            }
            mapString += "\n";
            // "\n" : 개행문자, Enter랑 같은 기능, 다음 줄
            // 내부 반복문이 끝나면 외부 반복문을 1 번씩 실행하므로
            // 내부 반복문이 끝나면 다음줄을 쓴다.
        }
        Debug.Log(mapString);
        
        for (int x = 0; x < mapTiles.GetLength(0); x++)
        {
            for (int y = 0; y < mapTiles.GetLength(1); y++)
            {
                // cubeMapTiles[x, y] = mapTiles[x, y] == 1 ? Instantiate(cube) : null;

                if (mapTiles[x, y] == 1)
                {
                    cubeMapTiles[x, y] = Instantiate(cube, new Vector3(x - 5, y - 5, 0), Quaternion.identity);
                }
                else
                {
                    cubeMapTiles[x, y] = Instantiate(sphere, new Vector3(x - 5, y - 5, 0), Quaternion.identity);
                }
            }
           
        }
        
        
    }
}
