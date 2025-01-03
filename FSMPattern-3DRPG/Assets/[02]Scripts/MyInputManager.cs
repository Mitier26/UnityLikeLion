using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyInputManager : Singleton<MyInputManager>
{
    private InputAction moveAction;
    private InputAction jumpAction;
    
    private InputAction number_1;
    private InputAction number_2;
    
    void Start()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        number_1 = playerInput.actions["Number_1"];
        number_2 = playerInput.actions["Number_2"];
        
        jumpAction = playerInput.actions["Jump"];
        moveAction = playerInput.actions["Move"];
    }
    
    void Update()
    {
        var playercController = PlayerController.Instance;
        
        playercController.OnReadValue("Move", moveAction.ReadValue<Vector2>());
        playercController.OnTriggered("Jump", jumpAction.triggered);
        playercController.OnTriggered("Number_1", number_1.triggered);
        playercController.OnTriggered("Number_2", number_2.triggered);
    }
}