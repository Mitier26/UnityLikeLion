using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree : MonoBehaviour
{
    // class 안의 class
    // 숨겨놓은 class, BinaryTree를 통해서만 접근 가능
    public class Node
    {
        // 하나의 노드를 만들었다.
        public int Data;        // 실질적인 데이터
        public Node Left;       // 왼쪽을 가리키는 것
        public Node Right;      // 오른쪽을 가리키는 것

        public Node(int data)
        {
            Data = data;
            Left = Right = null;
        }
    }

    private Node root;      // 머리

    // 전위 순회
    public void PreOrder(Node node)
    {
        if(node == null) return;

        // 재귀함수 
        // 자기 자신의 함수를 다시 사용
        Debug.Log(node.Data);
        PreOrder(node.Left);
        PreOrder(node.Right);
    }

    // 중위 순회
    public void Inorder(Node node)
    {
        if (node == null) return;
        
        PreOrder(node.Left);
        Debug.Log(node.Data);
        PreOrder(node.Right);
    }

    public void Postorder(Node node)
    {
        if (node == null) return;
        
        PreOrder(node.Left);
        PreOrder(node.Right);
        Debug.Log(node.Data);
    }
    
    private void Start()
    {
        Node root = new Node(100);
        root.Left = new Node(50);
        root.Left.Right = new Node(40);
        root.Left.Left = new Node(60);
        root.Right = new Node(110);
        
        // 실행 하기
        PreOrder(root);
        Inorder(root);
        Postorder(root);
    }
}
