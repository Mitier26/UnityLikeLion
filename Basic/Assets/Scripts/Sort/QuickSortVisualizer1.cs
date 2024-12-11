using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSortVisualizer1 : MonoBehaviour
{
    public GameObject cubePrefab;
    public float cubeSpacing = 1.5f;
    private GameObject[] cubes;

    private Color pivotColor = Color.red;
    private Color compareColor = Color.blue;
    private Color sortedColor = Color.green;
    private Color defaultColor = Color.white;

    public void CreateCubes(int[] arr)
    {
        cubes = new GameObject[arr.Length];
        for (int i = 0; i < arr.Length; i++)
        {
            GameObject cube = Instantiate(cubePrefab.gameObject, new Vector3(i * cubeSpacing, arr[i] / 2f, 0), Quaternion.identity);
            cube.transform.localScale = new Vector3(1, arr[i], 1);
            cubes[i] = cube;
        }
    }

    private void ChangeCubeColor(int index, Color color)
    {
        Renderer renderer = cubes[index].GetComponent<Renderer>();
        renderer.material.color = color;
    }

    public IEnumerator QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pi = -1;

            yield return StartCoroutine(Partition(arr, low, high, result => pi = result));

            ChangeCubeColor(pi, sortedColor);

            yield return StartCoroutine(QuickSort(arr, low, pi - 1));
            yield return StartCoroutine(QuickSort(arr, pi + 1, high));
        }
        else if (low == high)
        {
            ChangeCubeColor(low, sortedColor);
        }
    }

    private IEnumerator Partition(int[] arr, int low, int high, System.Action<int> callback)
    {
        int pivot = arr[high];
        ChangeCubeColor(high, pivotColor);
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            ChangeCubeColor(j, compareColor);

            if (arr[j] < pivot)
            {
                i++;
                Swap(arr, i, j);
                yield return SwapCubes(i, j);
            }

            ChangeCubeColor(j, defaultColor);
        }

        Swap(arr, i + 1, high);
        yield return SwapCubes(i + 1, high);

        ChangeCubeColor(high, defaultColor);
        ChangeCubeColor(i + 1, sortedColor);

        callback(i + 1);
    }

    private void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    private IEnumerator SwapCubes(int i, int j)
    {
        Vector3 tempPosition = cubes[i].transform.position;
        cubes[i].transform.position = cubes[j].transform.position;
        cubes[j].transform.position = tempPosition;

        GameObject tempCube = cubes[i];
        cubes[i] = cubes[j];
        cubes[j] = tempCube;

        yield return new WaitForSeconds(0.5f);
    }

    void Start()
    {
        // 정렬할 배열 정의
        int[] arr = { 10, 3, 7, 1, 9, 4, 2, 8, 5, 6 };

        // 큐브 생성
        CreateCubes(arr);

        // 퀵소트 시작
        StartCoroutine(QuickSort(arr, 0, arr.Length - 1));
    }
}
