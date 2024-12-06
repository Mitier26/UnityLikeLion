using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue : MonoBehaviour
{
        /// <summary>
    /// 제네릭 우선순위 큐 구현
    /// T는 반드시 IComparable<T> 인터페이스를 구현해야 함
    /// 최소 힙(Min Heap) 구조를 사용하여 구현됨
    /// </summary>
    public class PriorityQueue1<T> where T : IComparable<T>
    {
        // 힙 구조를 저장하기 위한 내부 리스트
        private List<T> heap = new List<T>();

        /// <summary>
        /// 우선순위 큐에 새로운 항목을 추가
        /// </summary>
        /// <param name="item">추가할 항목</param>
        public void Enqueue(T item)
        {
            // 새 항목을 힙의 마지막에 추가
            heap.Add(item);
            // 새로 추가된 항목의 인덱스
            int currentIndex = heap.Count - 1;
            // 힙 속성을 만족하도록 위로 재정렬
            HeapifyUp(currentIndex);
        }

        /// <summary>
        /// 우선순위가 가장 높은(값이 가장 작은) 항목을 제거하고 반환
        /// </summary>
        /// <returns>우선순위가 가장 높은 항목</returns>
        /// <exception cref="InvalidOperationException">큐가 비어있을 경우 발생</exception>
        public T Dequeue()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("우선순위 큐가 비어있습니다.");
            
            // 루트 노드(가장 작은 값)를 저장
            T root = heap[0];
            int lastIndex = heap.Count - 1;
            
            // 마지막 노드를 루트로 이동
            heap[0] = heap[lastIndex];
            // 마지막 노드 제거
            heap.RemoveAt(lastIndex);
            
            // 힙이 비어있지 않다면 힙 속성을 만족하도록 아래로 재정렬
            if (heap.Count > 0)
                HeapifyDown(0);
                
            return root;
        }

        /// <summary>
        /// 지정된 인덱스의 노드를 부모 노드와 비교하여 필요한 경우 위치를 교환
        /// 최소 힙 속성을 유지하기 위해 상향식으로 재정렬
        /// </summary>
        /// <param name="index">재정렬을 시작할 노드의 인덱스</param>
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                // 부모 노드의 인덱스 계산
                int parentIndex = (index - 1) / 2;
                
                // 현재 노드가 부모 노드보다 크거나 같으면 중단
                if (heap[index].CompareTo(heap[parentIndex]) >= 0)
                    break;
                    
                // 현재 노드가 부모 노드보다 작으면 위치 교환
                Swap(index, parentIndex);
                // 다음 비교를 위해 인덱스를 부모 인덱스로 업데이트
                index = parentIndex;
            }
        }

        /// <summary>
        /// 지정된 인덱스의 노드를 자식 노드들과 비교하여 필요한 경우 위치를 교환
        /// 최소 힙 속성을 유지하기 위해 하향식으로 재정렬
        /// </summary>
        /// <param name="index">재정렬을 시작할 노드의 인덱스</param>
        private void HeapifyDown(int index)
        {
            int lastIndex = heap.Count - 1;
            
            while (true)
            {
                int smallest = index;
                // 왼쪽 자식 노드의 인덱스 계산
                int leftChild = 2 * index + 1;
                // 오른쪽 자식 노드의 인덱스 계산
                int rightChild = 2 * index + 2;
                
                // 왼쪽 자식이 현재 노드보다 작으면 교환 대상으로 표시
                if (leftChild <= lastIndex && heap[leftChild].CompareTo(heap[smallest]) < 0)
                    smallest = leftChild;
                    
                // 오른쪽 자식이 현재 교환 대상보다 작으면 교환 대상으로 표시
                if (rightChild <= lastIndex && heap[rightChild].CompareTo(heap[smallest]) < 0)
                    smallest = rightChild;
                    
                // 교환이 필요 없으면 중단
                if (smallest == index)
                    break;
                    
                // 현재 노드와 가장 작은 자식 노드의 위치 교환
                Swap(index, smallest);
                // 다음 비교를 위해 인덱스 업데이트
                index = smallest;
            }
        }

        /// <summary>
        /// 힙 내의 두 노드의 위치를 교환
        /// </summary>
        /// <param name="i">첫 번째 노드의 인덱스</param>
        /// <param name="j">두 번째 노드의 인덱스</param>
        private void Swap(int i, int j)
        {
            T temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }

        /// <summary>
        /// 현재 우선순위 큐에 있는 항목의 개수를 반환
        /// </summary>
        public int Count => heap.Count;

        /// <summary>
        /// 우선순위 큐가 비어있는지 여부를 반환
        /// </summary>
        public bool IsEmpty => heap.Count == 0;
    }
}
