using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackNode<T>
{
    public T data;
    public StackNode<T> prev;
}

public class StackCustom<T> where T : new()
{
    public StackNode<T> top;

    public void Push(T data)
    {
        var stackNode = new StackNode<T>();

        stackNode.data = data;
        
        stackNode.prev = top;
        // 여기에서 top는 이전에 있던 top
        // 이전에 있는 top는 이제 나의 앞에 있으니
        // 나의.prev = top
        
        // 그리고
        top = stackNode;
        // 내가 top이 된다!!
    }

    // 가장 위에 것을 끄집어 낸다.
    public T Pop()
    {
        if (top == null)
        {
            return new T();
        }

        var result = top.data;
        top = top.prev;
        // top : 가장 마지막 것
        // CustomStack의 변수이다 <- top
        // 무엇이 top인지 저장!!

        return result;
    }

    // 가장 위에 것을 확인한다.
    public T Peek()
    {
        if (top == null)
        {
            return new T();
        }

        return top.data;
    }

    public int Count()
    {
        int count = 0;
        var node = top;
        while (node != null)
        {
            count++;
            node = node.prev;
        }

        return count;
    }
}

public class StackExample : MonoBehaviour
{
    private void Start()
    {
        StackCustom<int> stack = new StackCustom<int>();
        
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        
        Debug.Log(stack.Pop());
        
        Debug.Log(stack.Pop());
        Debug.Log(stack.Peek());
        
    }

    [NonSerialized]
    public float speed = 3.0f;
    // public 이지만 인스펙터에 보이지 않게 하는 것

    [SerializeField]
    private float speed2 = 3.0f;
    // private 이지만 인스펙터에 보이게 하는 것

    private StackCustom<Vector3> positionStack = new StackCustom<Vector3>();
    
    private void Update()
    {
        Vector3 movePos = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movePos += transform.forward;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            movePos -= transform.forward;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            movePos -= transform.right;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            movePos += transform.right;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) )
        {
            movePos = Vector3.zero;
            positionStack.Push(transform.position);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (positionStack.Count() > 0)
            {
                transform.position = positionStack.Pop();
            }
        }

        transform.position += movePos.normalized * (speed2 * Time.deltaTime);
    }
    
    void LateUpdate()
    {
        var node = positionStack.top;
        while (node != null)
        {
            Debug.DrawLine(node.data, node.data + Vector3.up, Color.red);
            node = node.prev;
        }
    }

    public bool AreaBreacketBalanced(string expression)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char c in expression)
        {
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            else if (c == ')' || c == ']' || c == '}')
            {
                if (stack.Count == 0)
                    return false;

                char top = stack.Pop();
                if (c == ')' && top != '(' || c == ']' && top != '[' || c == '}' && top != '{')
                {
                    return false;
                }
            }
        }

        return stack.Count == 0;
    }
}
