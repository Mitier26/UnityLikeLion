using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 노드의 색상을 정의하는 열거형
public enum NodeColor
{
    Red,
    Black
}

public class RedBlackTree : MonoBehaviour
{
    public RBNode nilNode;
    public RBNode root;
    public RBNode RBNodePrefab;

    public bool bNext = false;
    
    void Start()
    {
        root = nilNode;
    }

    void Update()
    {
        bNext = false;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bNext = true;
        }
        
        if (Input.GetKey(KeyCode.P))
        {
            bNext = true;
        }
    }

// 삽입 시 트리 재조정을 위한 좌회전
    private IEnumerator LeftRotate(RBNode x)
    {
        RBNode y = x.right;
        yield return new WaitUntil(() => bNext);
        x.right = y.left;
        yield return new WaitUntil(() => bNext);
        if (y.left != nilNode)
        {
            y.left.parent = x;
            yield return new WaitUntil(() => bNext);
        }
            
        y.parent = x.parent;
        yield return new WaitUntil(() => bNext);

        if (x.parent == nilNode)
        {
            root = y;   
            yield return new WaitUntil(() => bNext);
        }
        else if (x == x.parent.left)
        {
            x.parent.left = y;
            yield return new WaitUntil(() => bNext);
        }
        else
        {
            x.parent.right = y;
            yield return new WaitUntil(() => bNext);
        }
            
        y.left = x;
        yield return new WaitUntil(() => bNext);
        x.parent = y;
        yield return new WaitUntil(() => bNext);
    }

    // 삽입 시 트리 재조정을 위한 우회전
    private IEnumerator RightRotate(RBNode x)
    {
        RBNode y = x.left;
        yield return new WaitUntil(() => bNext);
        x.left = y.right;
        yield return new WaitUntil(() => bNext);

        if (y.right != nilNode)
        {
            y.right.parent = x;
            yield return new WaitUntil(() => bNext);
        }
            
        y.parent = x.parent;
        yield return new WaitUntil(() => bNext);

        if (x.parent == nilNode)
        {
            root = y;
            yield return new WaitUntil(() => bNext);
        }
        else if (x == x.parent.right)
        {
            x.parent.right = y;
            yield return new WaitUntil(() => bNext);
        }
        else
        {
            x.parent.left = y;
            yield return new WaitUntil(() => bNext);
        }
            
        y.right = x;
        yield return new WaitUntil(() => bNext);
        x.parent = y;
        yield return new WaitUntil(() => bNext);
    }

    // 노드 삽입
    public void Insert(int key)
    {
        RBNode node = Instantiate(RBNodePrefab.gameObject, transform).GetComponent<RBNode>();
        node.left = nilNode;
        node.right = nilNode;
        node.nilNode = nilNode;
        node.parent = nilNode;
        
        node.data = key;
        
        RBNode y = nilNode;
        RBNode x = root;

        // 삽입 위치 찾기
        while (x != nilNode)
        {
            y = x;
            if (node.data < x.data)
                x = x.left;
            else
                x = x.right;
        }

        node.parent = y;
        
        if (y == nilNode)
            root = node;  // 조건 2: 루트는 항상 Black이 되도록 InsertFixup에서 처리
        else if (node.data < y.data)
            y.left = node;
        else
            y.right = node;

        StartCoroutine(InsertFixup(node)); // 레드-블랙 트리 속성 복구
    }

    // 삽입 후 레드-블랙 트리 속성 복구
    private IEnumerator InsertFixup(RBNode k)
    {
        RBNode u = nilNode;
        // 조건 4: Red 노드의 자식은 Black이어야 함
        while (k.parent != nilNode && k.parent.color == NodeColor.Red)
        {
            if (k.parent == k.parent.parent.right)
            {
                u = k.parent.parent.left;
                // Case 1: 삼촌 노드가 Red인 경우
                if (u.color == NodeColor.Red)
                {
                    // 색상 변경으로 해결
                    u.color = NodeColor.Black;
                    yield return new WaitUntil(() => bNext);
                    k.parent.color = NodeColor.Black;
                    yield return new WaitUntil(() => bNext);
                    k.parent.parent.color = NodeColor.Red;
                    yield return new WaitUntil(() => bNext);
                    k = k.parent.parent;
                }
                else
                {
                    // Case 2 & 3: 삼촌 노드가 Black인 경우
                    if (k == k.parent.left)
                    {
                        k = k.parent;
                        yield return new WaitUntil(() => bNext);
                        yield return RightRotate(k);
                    }
                    // 색상 변경 및 회전으로 해결
                    k.parent.color = NodeColor.Black;
                    yield return new WaitUntil(() => bNext);
                    k.parent.parent.color = NodeColor.Red;
                    yield return new WaitUntil(() => bNext);
                    yield return LeftRotate(k.parent.parent);
                }
            }
            else
            {
                // 위의 경우의 대칭
                u = k.parent.parent.right;
                if (u.color == NodeColor.Red)
                {
                    u.color = NodeColor.Black;
                    yield return new WaitUntil(() => bNext);
                    k.parent.color = NodeColor.Black;
                    yield return new WaitUntil(() => bNext);
                    k.parent.parent.color = NodeColor.Red;
                    yield return new WaitUntil(() => bNext);
                    k = k.parent.parent;
                    yield return new WaitUntil(() => bNext);
                }
                else
                {
                    if (k == k.parent.right)
                    {
                        k = k.parent;
                        yield return LeftRotate(k);
                    }
                    k.parent.color = NodeColor.Black;
                    yield return new WaitUntil(() => bNext);
                    k.parent.parent.color = NodeColor.Red;
                    yield return new WaitUntil(() => bNext);
                    yield return RightRotate(k.parent.parent);
                }
            }
            if (k == root)
                break;
        }
        yield return new WaitUntil(() => bNext);
        // 조건 2: 루트는 항상 Black
        root.color = NodeColor.Black;
        
        Debug.Log("Insert Finish");
    }
}
