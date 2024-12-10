using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree : MonoBehaviour
{
    //   NODE      //
    //   (root)
    //  /     \
    //(left)(right)
    //             //
    
    public class Node
    {
        public int Data;
        public Node Left;
        public Node Right;

        public Node(int data)
        {
            Data = data;
            Left = Right = null;
        }
    }
    
    private Node root;

    public void PreOrder(Node node)
    {
        if (node == null)
            return;
        
        Debug.Log(node.Data);
        PreOrder(node.Left);
        PreOrder(node.Right);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        Node root = new Node(100);
        root.Left = new Node(50);
        root.Left.Left = new Node(40);
        root.Left.Right = new Node(60);
        root.Right = new Node(110);
        
        // 100 50 40 60 110
        PreOrder(root);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
