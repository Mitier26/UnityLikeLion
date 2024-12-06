using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueNode<T>{
    // 데이터가 저장되는 프로퍼티
    public T Data { get; set; }
    // 다음 노드를 가리키는 프로퍼티
    public QueueNode<T> Next { get; set; }

    // 생성자 : 초기화시 데이터 값을 설정
    public QueueNode(T data)
    {
        Data = data;
        Next = null;
    }
}

// 연결 리스트 기반의 큐
public class NodeQueue<T>
{
    // 큐의 앞,뒤를 나타내는 노드
    private QueueNode<T> front;
    private QueueNode<T> rear;
    
    // 큐의 크기
    private int size;

    public NodeQueue()
    {
        front = null;
        rear = null;
        size = 0;
    }

    // 큐의 뒤에 데이터를 추가하는 메서드
    public void Enqueue(T item)
    {
        // item이라는 데이터를 받아 새로온 node를 만듬
        QueueNode<T> newNode = new QueueNode<T>(item);
        // 위에 node에서 data와 next = null 있음

        // 큐가 비어 있다면
        if (IsEmpty())
        {
            // 앞과뒤를 새로 만든 node로 만든
            front = newNode;
            rear = newNode;
            // 처음 데이터가 들어 오면 아무것도 없다.
            // 이 때 처음과 끝은 새로 생긴 것이다.
        }
        else
        {
            // 데이터가 들어 있었다면
            rear.Next = newNode;
            // 있던 마지막의 다음에 새로운 것을 넣는다.
            rear = newNode;
            // 뒤를 새로운 것이 한다.
            // 순서가 중요.
            // 먼저 이전의 뒤에 있던 것의 다음을 변경해야한다. null 이던 것을
            // 그리고 뒤로 들어감
            // 혼동 스럽지만 순서에 주의
        }

        size++;
    }

    // 앞에 것을 빼는 메서드, 앞에 것을 제거하는 특징이 있다.
    public T Dequeue()
    {
        // 예외 처리 : 큐가 비어있으면 뺄 것이 없다.
        if (IsEmpty())
        {
            throw new InvalidOperationException("큐가 비어있습니다.");
        }
        
        // 반환할 데이터
        T data = front.Data;
        // 가장 앞에 있는 것을 데이터를 반환한다.
        front = front.Next;
        // 앞의 것은 사라지기 때문에 앞에 것은 앞에 것의 뒤의 것으로 대체한다.
        
        
        size--;

        // 앞에 있는 데이터를 뺐을 때 큐가 비어 있으면
        // 뒤가 없으면 null IsEmpty 는 size 기반이다.
        // 앞에 것을 뺐는데 size가 0 이면!! 뒤가 없다는 것
        if (IsEmpty())
        {
            rear = null;
        }

        return data;
    }

    // 앞의 것을 반환하지만 제거 하지 않음
    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("큐가 비어있습니다.");
        }
        // 앞의 것의 data를 반환
        return front.Data;
    }

    public bool IsEmpty()
    {
        return size == 0;
    }

    public int Size()
    {
        return size;
    }
}

// 우선 순위 큐
// IComparable : T 를 비교하기 위한 인터페이스 상속
public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> heap = new List<T>();

    public void Enqueye(T item)
    {
        heap.Add(item);
        int currentIndex = heap.Count - 1;
        HeapifyUp(currentIndex);
    }

    public T Dequeue()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("우선순위 큐가 비었다");

        T root = heap[0];
        int lastIndex = heap.Count - 1;
        heap[0] = heap[lastIndex];
        heap.RemoveAt(lastIndex);

        return root;
    }
    
    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;
            if (heap[index].CompareTo(heap[parentIndex]) >= 0)
                break;

            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    private void HeapifyDown(int index)
    {
        int lastIndex = heap.Count - 1;
        while (true)
        {
            int smallest = index;
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;

            if (leftChild <= lastIndex && heap[leftChild].CompareTo(heap[smallest]) < 0)
                smallest = leftChild;
            if (rightChild <= lastIndex && heap[rightChild].CompareTo(heap[smallest]) < 0)
                smallest = rightChild;

            if (smallest == index)
                break;

            Swap(index, smallest);
            index = smallest;
        }
    }

    private void Swap(int i, int j)
    {
        T temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }

    public int Count => heap.Count;
    public bool IsEmpty => heap.Count == 0;
}


public class QueueNodeExample : MonoBehaviour
{
    
}
