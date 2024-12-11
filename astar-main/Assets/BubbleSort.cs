using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BubbleSort : MonoBehaviour
{
    public Material swapMaterial;
    public Material pivotMaterial;
    public Material normalMaterial;
    
    public Bar barPrefab;
    
    public void _BubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    // 두 원소 교환
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                }
            }
        }
    }
    
    // [ 3, 5, 6, 9, 1, 2, 4, 11, 7 ] == length 9 
    // QuickSort(arr, 0, arr.Length - 1); 
    // QuickSort -> Partition call low 0 , high = 8
    // arr[8] -> pivot value 값
    // i는 위치 값이다. 0 - 1 이므로 -1로 시작한다.
    // j = low -> j < high -> j ++
    // arr[j] value 값이 pivot 값보다 작으면
    // i ++ 1 증가되서 0이되고
    // 0 번과 현재 j번의 위치를 교환한다.
    
    // i  j                        p 
    //  [ 3, 5, 6, 9, 1, 2, 4, 11, 7 ]
    //   ij(swap)                  p
    //  [ 3, 5, 6, 9, 1, 2, 4, 11, 7 ]
    //    i  j                     p
    //  [ 3, 5, 6, 9, 1, 2, 4, 11, 7 ]
    //      ij(swap)               p
    //  [ 3, 5, 6, 9, 1, 2, 4, 11, 7 ]
    //       i  j                  p
    //  [ 3, 5, 6, 9, 1, 2, 4, 11, 7 ]
    //         ij(swap)            p
    //  [ 3, 5, 6, 9, 1, 2, 4, 11, 7 ]
    //          i  j               p
    //  [ 3, 5, 6, 9, 1, 2, 4, 11, 7 ]
    //          i     j            p
    //  [ 3, 5, 6, 9, 1, 2, 4, 11, 7 ]
    //             i  j(swap)      p
    //  [ 3, 5, 6, 9, 1, 2, 4, 11, 7 ]
    //             i     j         p
    //  [ 3, 5, 6, 1, 9, 2, 4, 11, 7 ]
    //                i  j(swap)   p
    //  [ 3, 5, 6, 1, 9, 2, 4, 11, 7 ]
    //                   i  j(swap)p
    //  [ 3, 5, 6, 1, 2, 9, 4, 11, 7 ]
    //                   i  j      p
    //  [ 3, 5, 6, 1, 2, 4, 9, 11, 7 ]
    //                   i     j   p
    //  [ 3, 5, 6, 1, 2, 4, 9, 11, 7 ]
    //                      i      p(swap)
    //  [ 3, 5, 6, 1, 2, 4, 9, 11, 7 ]
    //  [ 3, 5, 6, 1, 2, 4, 7, 11, 9 ]

    // [ 3, 5, 6, 1, 2, 4] [7] [11, 9 ]

    private int pi = 0;
    
    private IEnumerator Partition(Bar[] arr, int low, int high)
    {
        int pivot = arr[high]._data; 
        int i = (low - 1);

        for (int j = low; j < high; j++)
        {
            if (arr[j]._data < pivot)
            {
                i++;
           
                (arr[i], arr[j]) = (arr[j], arr[i]);
                arr[j].SetData(j);
                arr[i].SetData(i);
                arr[j]._stick.GetComponent<MeshRenderer>().material = swapMaterial;
                arr[i]._stick.GetComponent<MeshRenderer>().material = swapMaterial;

                var i1 = i;
                var j1 = j;
                yield return new WaitUntil(() => arr[i1].bResizeFinish && arr[j1].bResizeFinish);
                
                arr[j]._stick.GetComponent<MeshRenderer>().material = normalMaterial;
                arr[i]._stick.GetComponent<MeshRenderer>().material = normalMaterial;
            }
        }
        
        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
        arr[i + 1].SetData(i + 1);
        arr[high].SetData(high);
        arr[i + 1]._stick.GetComponent<MeshRenderer>().material = swapMaterial;
        arr[high]._stick.GetComponent<MeshRenderer>().material = swapMaterial;

        yield return new WaitUntil(() => arr[i + 1].bResizeFinish && arr[high].bResizeFinish);

        arr[i + 1]._stick.GetComponent<MeshRenderer>().material = normalMaterial;
        arr[high]._stick.GetComponent<MeshRenderer>().material = normalMaterial;
        
        arr[pi]._stick.GetComponent<MeshRenderer>().material = normalMaterial;
        pi = i + 1;
        arr[pi]._stick.GetComponent<MeshRenderer>().material = pivotMaterial;
    }
    
    public IEnumerator QuickSort(Bar[] arr, int low, int high)
    {
        if (low < high)
        {
            yield return StartCoroutine(Partition(arr, low, high));
            yield return StartCoroutine(QuickSort(arr, low, pi - 1));
            yield return StartCoroutine(QuickSort(arr, pi + 1, high));
        }
    }

    void Start()
    {
        int[] arr = { 3, 5, 6, 9, 1, 2, 4, 11, 7 };
        
        Bar[] instanceArr = new Bar[arr.Length];
        for (var i = 0; i < arr.Length; i++)
        {
            instanceArr[i] = Instantiate(barPrefab, transform);
            instanceArr[i].SetData(i);
            instanceArr[i]._data = arr[i];
        }
        
        //_BubbleSort(arr);
        StartCoroutine(QuickSort(instanceArr, 0, arr.Length - 1));
        foreach (var i in instanceArr)
        {
            Debug.Log(i._data);    
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
