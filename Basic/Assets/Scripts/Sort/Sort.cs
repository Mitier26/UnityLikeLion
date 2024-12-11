using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sort : MonoBehaviour
{
    public void BubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    // 두 원소 교환
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;

                    // (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                }
            }
        }
    }
    // [ 3, 6, 1, 4, 33, 7, 32, 85, 34, 21, 76, 41, 39 ]
    // QuickSort -> Partition 실행 순서
    // QuickSort(arr, 0, arr.Length-1);
    // low : 0, high : 39
    // arr[12] 값은 pivot value
    // i 는 위치 -1 부터 시작
    // arr[j] value 값이 pivot 값보 작으면
    // i++ 1  증가되서 0이 됨
    // 0번과 현재 j번의 위치를 교화
    
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
    public void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(arr, low, high);
            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }
    }

    private int Partition(int[] arr, int low, int high)
    {
        // pivot을 구하는 것
        
        int pivot = arr[high];
        int i = (low - 1);

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }

        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);

        return i + 1;
    }

    private void Start()
    {
        int[] arr = { 3, 5, 6, 9, 1, 2, 4, 11, 7 };
        // BubbleSort(arr);
        QuickSort(arr, 0, arr.Length-1);
    }
}
