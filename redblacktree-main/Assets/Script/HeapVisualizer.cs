
// HeapVisualizer.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeapVisualizer : MonoBehaviour
{
    public GameObject nodePrefab;
    public Transform nodesContainer;
    public Button insertButton;
    public Button extractButton;
    public TMPro.TMP_InputField inputField;
    public LineRenderer lineRendererPrefab;  // 노드 간 연결선을 그리기 위한 프리팹

    private List<HeapNode> nodes = new List<HeapNode>();
    private List<LineRenderer> lines = new List<LineRenderer>();
    private MaxHeap heap;
    
    private float horizontalSpacing = 3f;
    private float verticalSpacing = 2f;
    private Vector2 rootPosition = new Vector2(0, 0);

    private void Start()
    {
        heap = new MaxHeap(15);
        heap.OnHeapUpdated += UpdateHeapVisualization;
        
        insertButton.onClick.AddListener(() => {
            if (int.TryParse(inputField.text, out int value))
            {
                InsertWithVisualization(value);
                inputField.text = "";
            }
        });

        extractButton.onClick.AddListener(ExtractMinWithVisualization);
    }

    private void InsertWithVisualization(int value)
    {
        GameObject nodeObj = Instantiate(nodePrefab, nodesContainer);
        HeapNode node = nodeObj.GetComponent<HeapNode>();
        node.SetValue(value);
        nodes.Add(node);

        heap.Insert(value);
    }

    private void ExtractMinWithVisualization()
    {
        if (nodes.Count > 0)
        {
            nodes[0].Highlight();
            Destroy(nodes[0].gameObject);
            nodes.RemoveAt(0);

            heap.ExtractMax();
            UpdateLines();
        }
    }

    // HeapVisualizer.cs의 UpdateHeapVisualization 메서드를 수정
    private void UpdateHeapVisualization(int[] heapArray)
    {
        // 기존 노드들의 GameObject 제거
        foreach (var node in nodes)
        {
            if (node != null)
                Destroy(node.gameObject);
        }
        nodes.Clear();

        // 힙 배열의 각 요소에 대해 새 노드 생성
        for (int i = 0; i < heapArray.Length; i++)
        {
            GameObject nodeObj = Instantiate(nodePrefab, nodesContainer);
            HeapNode node = nodeObj.GetComponent<HeapNode>();
            node.SetValue(heapArray[i]);
            nodes.Add(node);
            node.MoveTo(CalculateNodePosition(i));
        }

        UpdateLines();
    }

    private Vector3 CalculateNodePosition(int index)
    {
        int level = Mathf.FloorToInt(Mathf.Log(index + 1, 2));
        int levelStartIndex = (1 << level) - 1;
        int positionInLevel = index - levelStartIndex;
        int nodesInLevel = 1 << level;
    
        float xPos = (positionInLevel - (nodesInLevel - 1) / 2.0f) * horizontalSpacing;
        float yPos = -level * verticalSpacing;
    
        return new Vector3(xPos, yPos, 0);
    }


    private void UpdateLines()
    {
        // 기존 라인 제거
        foreach (var line in lines)
        {
            if (line != null)
                Destroy(line.gameObject);
        }
        lines.Clear();

        // 새로운 라인 생성
        for (int i = 0; i < nodes.Count; i++)
        {
            int leftChild = 2 * i + 1;
            int rightChild = 2 * i + 2;

            if (leftChild < nodes.Count)
            {
                LineRenderer line = Instantiate(lineRendererPrefab, nodesContainer);
                Vector3 startPos = nodes[i].transform.position;
                Vector3 endPos = nodes[leftChild].transform.position;
            
                line.positionCount = 2;
                line.SetPosition(0, startPos);
                line.SetPosition(1, endPos);
                line.startWidth = 0.1f;
                line.endWidth = 0.1f;
                lines.Add(line);
            }

            if (rightChild < nodes.Count)
            {
                LineRenderer line = Instantiate(lineRendererPrefab, nodesContainer);
                Vector3 startPos = nodes[i].transform.position;
                Vector3 endPos = nodes[rightChild].transform.position;
            
                line.positionCount = 2;
                line.SetPosition(0, startPos);
                line.SetPosition(1, endPos);
                line.startWidth = 0.1f;
                line.endWidth = 0.1f;
                lines.Add(line);
            }
        }
    }

    private void CreateLine(int parentIndex, int childIndex)
    {
        LineRenderer line = Instantiate(lineRendererPrefab, nodesContainer);
        line.positionCount = 2;
        line.SetPosition(0, nodes[parentIndex].transform.position);
        line.SetPosition(1, nodes[childIndex].transform.position);
        lines.Add(line);
    }

    private void OnDestroy()
    {
        if (heap != null)
            heap.OnHeapUpdated -= UpdateHeapVisualization;
    }
}