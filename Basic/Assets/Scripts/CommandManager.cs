using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 커맨드 패턴
    
// 인터페이스
// 인터페이스를 상속 받은 클래스는 인터페이스에 선언한 것은 구현해야 한다.
// 인터페이스는 다중 상속이 가능 하다.
public interface ICommand
{
    void Execute();
    void Undo();
    // 여기에서는 함수 선언만 하고 구현은 상속 받는 것에서 해야 한다.
}

public class CommandManager : MonoBehaviour
{

    private Stack<ICommand> undoStack = new Stack<ICommand>();
    private Stack<ICommand> redoStack = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        // 커맨드 객체의 execute를 실행한다.
        command.Execute();
        // 언두했을 때 command의 undo를 호출하기 위해 undo stack에 넣는다.
        undoStack.Push(command);
        // executeCommand 가 호출되면 가장 최신의 작업을 한것 이므로 redoStack은 비운다.
        redoStack.Clear();
    }

    public void Undo()
    {
        if (undoStack.Count > 0)
        {
            // 가장 최근에 실행 된 커맨드를 가져와서
            ICommand command = undoStack.Pop();
            // Undo시킨다.
            command.Undo();
            // 그리고 다시redo 할수 있기 때문에 redoStack에 넣는다.
            redoStack.Push(command);
        }
    }

    public void Redo()
    {
        if (redoStack.Count > 0)
        {
            // 가장 최근에 언두 된 커맨드를 가져와서
            ICommand command = redoStack.Pop();
            // Execute해준다.
            command.Execute();
            // 그리고 다시 Undo할수 있도록 undoStack에 넣는다.
            undoStack.Push(command);
        }
    }
    

    public float Speed = 3.0f;
    public float RotateSpeed = 3.0f;
    
    public Vector3 MoveDelta = Vector3.zero;
    public Vector3 RotateDelta = Vector3.zero;

    private bool prevMoved = false;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 movePos = Vector3.zero;
        Vector3 deltaRot = Vector3.zero;

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

        if (Input.GetKey(KeyCode.E))
        {
            deltaRot += transform.right * (Time.deltaTime * RotateSpeed);
        }
        
        if (Input.GetKey(KeyCode.Q))
        {
            deltaRot -= transform.right * (Time.deltaTime * RotateSpeed);
        }
        
        Vector3 addtivePosition = movePos.normalized * Speed * Time.deltaTime;
        
        // 움직였던 정보를 기록하기 위해 키를 땔때마다 위치를 기록한다.
        if (movePos == Vector3.zero && MoveDelta != Vector3.zero)
        {
            var moveCommand = new MoveCommand(transform, transform.position - MoveDelta);
            ExecuteCommand(moveCommand);
            MoveDelta = Vector3.zero;
            return;
        }

        if (deltaRot == Vector3.zero && RotateDelta != Vector3.zero)
        {
            var rotateCommand = new RotateCommand(transform, Quaternion.LookRotation(transform.forward - RotateDelta, Vector3.up));
            ExecuteCommand(rotateCommand);
            RotateDelta = Vector3.zero;
            return;
        }

        // 왔던 포지션으로 되돌아가는 코드
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Undo();
            return;
        }
        
        transform.position += addtivePosition;
        transform.rotation = Quaternion.LookRotation(transform.forward + deltaRot, Vector3.up);
        
        MoveDelta += addtivePosition;
        RotateDelta += deltaRot;
    }
}

public class MoveCommand : ICommand
{
    private Transform _transform;
    private Vector3 _oldPosition;
    private Vector3 _newPosition;

    public MoveCommand(Transform transform, Vector3 rollbackPosition)
    {
        // 이동하려는 트랜스폼 객체를 참조한다.
        _transform = transform;
        // 연두할때 돌아갈 포지션을 저장한다.
        _oldPosition = rollbackPosition;
        // execute시에 셋팅될 포지션 값을 저장한다.
        _newPosition = transform.position;
    }
    
    public void Execute()
    {
        // newPosition으로 갱신한다.
        _transform.position = _newPosition;
    }

    public void Undo()
    {
        // oldPosition으로 undo한다.
        _transform.position = _oldPosition;
    }
}

public class RotateCommand : ICommand
{
    private Transform _transform;
    private Quaternion _oldRotation;
    private Quaternion _newRotation;

    public RotateCommand(Transform transform, Quaternion rollbackRotation)
    {
        // 이동하려는 트랜스폼 객체를 참조한다.
        _transform = transform;
        // 연두할때 돌아갈 회전을 저장한다.
        _oldRotation = rollbackRotation;
        // execute시에 셋팅될 회전 값을 저장한다.
        _newRotation = _transform.rotation;
    }
    
    public void Execute()
    {
        // newRotation으로 갱신한다.
        _transform.rotation = _newRotation;
    }

    public void Undo()
    {
        // oldRotation으로 undo한다.
        _transform.rotation = _oldRotation;
    }
}