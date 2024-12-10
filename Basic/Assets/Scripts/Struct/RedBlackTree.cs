using UnityEngine;

public class RedBlackTree : MonoBehaviour
{
    // 노드의 색상을 정의하는 열거형
    private enum NodeColor
    {
        Red,
        Black
    }

    // 노드 클래스 정의
    private class Node
    {
        public int data;
        public Node left, right, parent;
        public NodeColor color;

        // 새로운 노드는 항상 Red로 생성 (조건 1 관련)
        public Node(int data)
        {
            this.data = data;
            left = right = parent = null;
            color = NodeColor.Red;
        }
    }

    private Node root;
    private Node TNULL; // NIL 노드 (조건 3 관련)

    void Start()
    {
        // NIL 노드는 항상 Black (조건 3)
        TNULL = new Node(0);
        TNULL.color = NodeColor.Black;
        root = TNULL;
    }

    // 삽입 시 트리 재조정을 위한 좌회전
    private void LeftRotate(Node x)
    {
        Node y = x.right;
        x.right = y.left;
        
        if (y.left != TNULL)
            y.left.parent = x;
            
        y.parent = x.parent;
        
        if (x.parent == null)
            root = y;
        else if (x == x.parent.left)
            x.parent.left = y;
        else
            x.parent.right = y;
            
        y.left = x;
        x.parent = y;
    }

    // 삽입 시 트리 재조정을 위한 우회전
    private void RightRotate(Node x)
    {
        Node y = x.left;
        x.left = y.right;
        
        if (y.right != TNULL)
            y.right.parent = x;
            
        y.parent = x.parent;
        
        if (x.parent == null)
            root = y;
        else if (x == x.parent.right)
            x.parent.right = y;
        else
            x.parent.left = y;
            
        y.right = x;
        x.parent = y;
    }

    // 노드 삽입
    public void Insert(int key)
    {
        Node node = new Node(key);
        node.left = TNULL;
        node.right = TNULL;

        Node y = null;
        Node x = root;

        // 삽입 위치 찾기
        while (x != TNULL)
        {
            y = x;
            if (node.data < x.data)
                x = x.left;
            else
                x = x.right;
        }

        node.parent = y;
        
        if (y == null)
            root = node;  // 조건 2: 루트는 항상 Black이 되도록 InsertFixup에서 처리
        else if (node.data < y.data)
            y.left = node;
        else
            y.right = node;

        InsertFixup(node); // 레드-블랙 트리 속성 복구
    }

    // 삽입 후 레드-블랙 트리 속성 복구
    private void InsertFixup(Node k)
    {
        Node u;
        // 조건 4: Red 노드의 자식은 Black이어야 함
        while (k.parent != null && k.parent.color == NodeColor.Red)
        {
            if (k.parent == k.parent.parent.right)
            {
                u = k.parent.parent.left;
                // Case 1: 삼촌 노드가 Red인 경우
                if (u.color == NodeColor.Red)
                {
                    // 색상 변경으로 해결
                    u.color = NodeColor.Black;
                    k.parent.color = NodeColor.Black;
                    k.parent.parent.color = NodeColor.Red;
                    k = k.parent.parent;
                }
                else
                {
                    // Case 2 & 3: 삼촌 노드가 Black인 경우
                    if (k == k.parent.left)
                    {
                        k = k.parent;
                        RightRotate(k);
                    }
                    // 색상 변경 및 회전으로 해결
                    k.parent.color = NodeColor.Black;
                    k.parent.parent.color = NodeColor.Red;
                    LeftRotate(k.parent.parent);
                }
            }
            else
            {
                // 위의 경우의 대칭
                u = k.parent.parent.right;
                if (u.color == NodeColor.Red)
                {
                    u.color = NodeColor.Black;
                    k.parent.color = NodeColor.Black;
                    k.parent.parent.color = NodeColor.Red;
                    k = k.parent.parent;
                }
                else
                {
                    if (k == k.parent.right)
                    {
                        k = k.parent;
                        LeftRotate(k);
                    }
                    k.parent.color = NodeColor.Black;
                    k.parent.parent.color = NodeColor.Red;
                    RightRotate(k.parent.parent);
                }
            }
            if (k == root)
                break;
        }
        // 조건 2: 루트는 항상 Black
        root.color = NodeColor.Black;
    }
}