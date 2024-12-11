using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuickSortVisualizer : MonoBehaviour
{
    public int len = 10; // 배열 길이
    public GameObject numCubePrefab; // 숫자 큐브 프리팹
    public float cubeSpacing = 2.0f; // 큐브 간격
    public float animationSpeed = 1.0f; // 애니메이션 속도
    public Color selectedColor = Color.red; // 선택된 큐브 색상
    public Color normalColor = Color.white; // 기본 큐브 색상

    private List<GameObject> cubes = new List<GameObject>(); // 큐브 리스트
    private int[] arr; // 숫자 배열

    private void Start()
    {
        // 랜덤 숫자 배열 생성
        arr = InitializeNumber(len);

        // 숫자 배열을 기반으로 큐브 생성
        CreateCubes(arr);

        // 퀵 정렬 실행
        StartCoroutine(QuickSort(arr, 0, arr.Length - 1));
    }

    private int[] InitializeNumber(int length)
    {
        int[] arr = new int[length];
        for (int i = 0; i < length; i++)
        {
            arr[i] = Random.Range(1, 100);
        }

        return arr;
    }

    private void CreateCubes(int[] arr)
    {
        float startX = -((arr.Length - 1) * cubeSpacing) / 2.0f;
        for (int i = 0; i < arr.Length; i++)
        {
            GameObject cube = Instantiate(
                numCubePrefab,
                new Vector3(startX + i * cubeSpacing, 0, 0),
                Quaternion.identity,
                transform
            );

            TextMeshPro textMesh = cube.GetComponentInChildren<TextMeshPro>();
            if (textMesh != null)
            {
                textMesh.text = arr[i].ToString();
            }

            cubes.Add(cube);
        }
    }

    private IEnumerator QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pi = 0; // 피벗 값 저장
            yield return StartCoroutine(Partition(arr, low, high, result => pi = result)); // Partition 결과를 콜백으로 받음
            yield return StartCoroutine(QuickSort(arr, low, pi - 1));
            yield return StartCoroutine(QuickSort(arr, pi + 1, high));
        }
    }

    private IEnumerator Partition(int[] arr, int low, int high, System.Action<int> callback)
    {
        int pivot = arr[high];
        int i = low - 1;

        HighlightCube(high, true); // Pivot 강조

        for (int j = low; j < high; j++)
        {
            HighlightCube(j, true); // 현재 비교 중인 큐브 강조

            if (arr[j] < pivot)
            {
                i++;
                // Swap and animate
                HighlightCube(i, true); // 교환 대상 강조
                yield return StartCoroutine(SwapCubes(i, j));
                (arr[i], arr[j]) = (arr[j], arr[i]);
                HighlightCube(i, false); // 강조 해제
            }

            HighlightCube(j, false); // 비교 완료 후 강조 해제
        }

        // Swap pivot and animate
        HighlightCube(i + 1, true); // 피벗 교환 강조
        yield return StartCoroutine(SwapCubes(i + 1, high));
        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);

        HighlightCube(i + 1, false); // 피벗 교환 후 강조 해제
        HighlightCube(high, false); // 피벗 해제

        callback(i + 1); // 피벗 위치 반환
        yield return null;
    }

    private IEnumerator SwapCubes(int indexA, int indexB)
    {
        Vector3 posA = cubes[indexA].transform.position;
        Vector3 posB = cubes[indexB].transform.position;

        // Smoothly swap positions
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * animationSpeed;
            cubes[indexA].transform.position = Vector3.Lerp(posA, posB, t);
            cubes[indexB].transform.position = Vector3.Lerp(posB, posA, t);
            yield return null;
        }

        // Update cube list
        (cubes[indexA], cubes[indexB]) = (cubes[indexB], cubes[indexA]);
    }

    private void HighlightCube(int index, bool highlight)
    {
        Renderer cubeRenderer = cubes[index].GetComponent<Renderer>();
        if (cubeRenderer != null)
        {
            cubeRenderer.material.color = highlight ? selectedColor : normalColor;
        }
    }
}
