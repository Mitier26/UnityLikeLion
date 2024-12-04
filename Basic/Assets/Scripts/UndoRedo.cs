using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoRedo : MonoBehaviour
{
    public float speed = 5f;

    private Stack<Vector3> objStack = new Stack<Vector3>();
    private Stack<Vector3> undoStack = new Stack<Vector3>();

    Vector3 movePos = Vector3.zero;
    
    private void Update()
    {
        Move();
        ObjectStack();
        Redo();
        Undo();
    }

    private void Move()
    {
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
        
        transform.position += movePos.normalized * (speed * Time.deltaTime);
    }

    private void ObjectStack()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) )
        {
            movePos = Vector3.zero;
            objStack.Push(transform.position);
        }
    }

    private void Redo()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (objStack.Count > 0)
            {
                undoStack.Push(transform.position);
                transform.position = objStack.Pop();
            }
        }
    }

    private void Undo()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (undoStack.Count > 0)
            {
                objStack.Push(transform.position);
                transform.position = undoStack.Pop();
            }
        }
    }
}
