using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class BubbleSortVisualizer : MonoBehaviour
{
    public int len = 10; // 배열 길이
    public GameObject numCubePrefab; // 숫자를 표시할 큐브 프리팹
    public float animationSpeed = 1.0f; // 애니메이션 속도

    private List<GameObject> cubes = new List<GameObject>(); // 생성된 큐브 리스트
    private int[] arr; // 숫자 배열
    public float cubeSpacing = 2.0f; // 큐브 간격
    
    private void Start()
    {
        // 랜덤 숫자 배열 생성
        arr = InitializeNumber(len);

        // 숫자 배열을 기반으로 큐브 생성
        CreateCubes(arr);

        // 버블 정렬 실행
        StartCoroutine(BubbleSortAnimation());
    }

    // 숫자 배열 초기화
    public int[] InitializeNumber(int length)
    {
        int[] arr = new int[length];
        for (int i = 0; i < length; i++)
        {
            arr[i] = Random.Range(1, 100); // 1부터 99까지의 랜덤 숫자
        }
        return arr;
    }

    // 큐브 생성
    private void CreateCubes(int[] arr)
    {
        float startX = -((arr.Length - 1) * cubeSpacing) / 2.0f; // 큐브들이 중앙 정렬되도록 시작 위치 조정
        for (int i = 0; i < arr.Length; i++)
        {
            // 큐브 생성
            GameObject cube = Instantiate(
                numCubePrefab,
                new Vector3(startX + i * cubeSpacing, 0, 0), // X 좌표 간격 추가
                Quaternion.identity,
                transform
            );
            cubes.Add(cube);

            // 큐브의 숫자 설정
            TextMeshPro textMesh = cube.GetComponentInChildren<TextMeshPro>();
            if (textMesh != null)
            {
                textMesh.text = arr[i].ToString();
            }
        }
    }

    // 버블 정렬 애니메이션
    private IEnumerator BubbleSortAnimation()
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                // 비교 대상 표시 (색상 변경 등 가능)
                HighlightCubes(j, j + 1, true);

                // 잠시 대기
                yield return new WaitForSeconds(animationSpeed);

                // 두 큐브를 비교하여 정렬
                if (arr[j] > arr[j + 1])
                {
                    // 배열 데이터 교환
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);

                    // 시각적 교환
                    yield return SwapCubes(cubes[j], cubes[j + 1]);

                    // 큐브 리스트 갱신
                    (cubes[j], cubes[j + 1]) = (cubes[j + 1], cubes[j]);
                }

                // 비교 대상 해제
                HighlightCubes(j, j + 1, false);
            }
        }
    }

    // 두 큐브의 위치를 애니메이션으로 교환
    private IEnumerator SwapCubes(GameObject cubeA, GameObject cubeB)
    {
        Vector3 positionA = cubeA.transform.position;
        Vector3 positionB = cubeB.transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < animationSpeed)
        {
            cubeA.transform.position = Vector3.Lerp(positionA, positionB, elapsedTime / animationSpeed);
            cubeB.transform.position = Vector3.Lerp(positionB, positionA, elapsedTime / animationSpeed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종 위치 설정
        cubeA.transform.position = positionB;
        cubeB.transform.position = positionA;
    }

    // 큐브 강조 (예: 색상 변경)
    private void HighlightCubes(int indexA, int indexB, bool highlight)
    {
        Color color = highlight ? Color.red : Color.white;

        cubes[indexA].GetComponent<Renderer>().material.color = color;
        cubes[indexB].GetComponent<Renderer>().material.color = color;
    }
}
