// HeapNode.cs
using UnityEngine;
using TMPro;

public class HeapNode : MonoBehaviour
{
    public TextMeshPro valueText;
    public SpriteRenderer nodeSprite;

    public void SetValue(int value)
    {
        valueText.text = value.ToString();
    }

    public void SetValueWithAnimation(int value)
    {
        PulseAnimation();
        SetValue(value);
    }

    public void MoveTo(Vector3 position)
    {
        transform.position = position;
    }

    public void Highlight()
    {
        nodeSprite.color = Color.yellow;
        Invoke(nameof(ResetColor), 0.5f);
    }

    public void HighlightAsSwap()
    {
        nodeSprite.color = Color.green;
        Invoke(nameof(ResetColor), 0.5f);
    }

    public void HighlightAsComparison()
    {
        nodeSprite.color = Color.cyan;
        Invoke(nameof(ResetColor), 0.3f);
    }

    private void ResetColor()
    {
        nodeSprite.color = Color.white;
    }

    public void PulseAnimation()
    {
        // LeanTween.scale(gameObject, Vector3.one * 1.2f, 0.2f)
        //          .setEasePunch()
        //          .setOnComplete(() => {
        //              transform.localScale = Vector3.one;
        //          });
    }
}

public class MaxHeap
{
    private int[] heap;
    private int size;
    private int capacity;
    
    public System.Action<int[]> OnHeapUpdated;

    public MaxHeap(int capacity)
    {
        this.capacity = capacity;
        this.size = 0;
        this.heap = new int[capacity];
    }

    private int Parent(int index) => (index - 1) / 2;
    private int LeftChild(int index) => 2 * index + 1;
    private int RightChild(int index) => 2 * index + 2;

    public void Insert(int value)
    {
        if (size >= capacity)
        {
            throw new System.InvalidOperationException("힙이 가득 찼습니다.");
        }

        heap[size] = value;
        int current = size;
        size++;

        // 부모보다 큰 값이면 위로 이동 (최대 힙으로 변경)
        HeapifyUp(current);
        
        OnHeapUpdated?.Invoke(GetHeapArray());
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = Parent(index);
            // 현재 노드가 부모 노드보다 크면 교환 (부등호 방향 변경)
            if (heap[index] > heap[parentIndex])
            {
                Swap(index, parentIndex);
                index = parentIndex;
            }
            else
            {
                break;
            }
        }
    }

    private void HeapifyDown(int index)
    {
        int largest = index;
        int left = LeftChild(index);
        int right = RightChild(index);

        // 왼쪽 자식이 더 큰 경우 (부등호 방향 변경)
        if (left < size && heap[left] > heap[largest])
        {
            largest = left;
        }

        // 오른쪽 자식이 더 큰 경우 (부등호 방향 변경)
        if (right < size && heap[right] > heap[largest])
        {
            largest = right;
        }

        if (largest != index)
        {
            Swap(index, largest);
            HeapifyDown(largest);
        }
    }

    public int ExtractMax()  // ExtractMin에서 ExtractMax로 이름 변경
    {
        if (size <= 0)
        {
            throw new System.InvalidOperationException("힙이 비어있습니다.");
        }

        int max = heap[0];  // max로 변수명 변경
        heap[0] = heap[size - 1];
        size--;

        if (size > 0)
            HeapifyDown(0);

        OnHeapUpdated?.Invoke(GetHeapArray());
        
        return max;
    }

    private void Swap(int i, int j)
    {
        (heap[i], heap[j]) = (heap[j], heap[i]);
    }

    public int[] GetHeapArray()
    {
        int[] currentHeap = new int[size];
        System.Array.Copy(heap, currentHeap, size);
        return currentHeap;
    }

    public int GetMax() => size > 0 ? heap[0] : throw new System.InvalidOperationException("힙이 비어있습니다.");
    public int GetSize() => size;
    public bool IsEmpty() => size == 0;
}