using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVLTreeVisualizer : MonoBehaviour
{
    private class Node
    {
        public int data;
        public Node left;
        public Node right;
        public int height;
        public Vector2 position;

        public Node(int data)
        {
            this.data = data;
            this.height = 1;
        }
    }

    private Node root;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 9; i++)
        {
            Insert(i);
        }
        UpdatePositions(root, 0, 0, 2);
    }

    // Insert function
    public void Insert(int data)
    {
        root = InsertRec(root, data);
    }

    private Node InsertRec(Node node, int data)
    {
        if (node == null)
            return new Node(data);

        if (data < node.data)
            node.left = InsertRec(node.left, data);
        else if (data > node.data)
            node.right = InsertRec(node.right, data);
        else
            return node;

        UpdateHeight(node);
        return Balance(node, data);
    }

    private int Height(Node node)
    {
        return node == null ? 0 : node.height;
    }

    private int GetBalance(Node node)
    {
        return node == null ? 0 : Height(node.left) - Height(node.right);
    }

    private void UpdateHeight(Node node)
    {
        if (node != null)
            node.height = Mathf.Max(Height(node.left), Height(node.right)) + 1;
    }

    private Node RightRotate(Node y)
    {
        Node x = y.left;
        Node T2 = x.right;

        x.right = y;
        y.left = T2;

        UpdateHeight(y);
        UpdateHeight(x);

        return x;
    }

    private Node LeftRotate(Node x)
    {
        Node y = x.right;
        Node T2 = y.left;

        y.left = x;
        x.right = T2;

        UpdateHeight(x);
        UpdateHeight(y);

        return y;
    }

    private Node Balance(Node node, int data)
    {
        int balance = GetBalance(node);

        if (balance > 1 && data < node.left.data)
            return RightRotate(node);

        if (balance < -1 && data > node.right.data)
            return LeftRotate(node);

        if (balance > 1 && data > node.left.data)
        {
            node.left = LeftRotate(node.left);
            return RightRotate(node);
        }

        if (balance < -1 && data < node.right.data)
        {
            node.right = RightRotate(node.right);
            return LeftRotate(node);
        }

        return node;
    }

    private void UpdatePositions(Node node, float x, float y, float horizontalSpacing)
    {
        if (node == null) return;

        node.position = new Vector2(x, y);

        UpdatePositions(node.left, x - horizontalSpacing, y - 1.5f, horizontalSpacing / 2);
        UpdatePositions(node.right, x + horizontalSpacing, y - 1.5f, horizontalSpacing / 2);
    }

    private void OnDrawGizmos()
    {
        if (root != null)
        {
            DrawNode(root);
        }
    }

    private void DrawNode(Node node)
    {
        if (node == null) return;

        // Draw connections to children
        if (node.left != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(new Vector3(node.position.x, node.position.y, 0), 
                            new Vector3(node.left.position.x, node.left.position.y, 0));
        }

        if (node.right != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(new Vector3(node.position.x, node.position.y, 0), 
                            new Vector3(node.right.position.x, node.right.position.y, 0));
        }

        // Draw the node
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(new Vector3(node.position.x, node.position.y, 0), 0.2f);

        // Draw node label
        UnityEditor.Handles.Label(new Vector3(node.position.x, node.position.y + 0.3f, 0), 
                                  $"   {node.data} (h:{node.height})");

        // Recursively draw children
        DrawNode(node.left);
        DrawNode(node.right);
    }
}
