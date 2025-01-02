using UnityEngine;
using UnityEngine.InputSystem;

public class Blackboard_Default : MonoBehaviour, IBlackboardBase
{
    public float jumpForce = 3f;
    public float moveSpeed = 3f;
    
    public Animator animator;
    public Rigidbody rigidbody;
    public InputAction moveInput;
    public InputAction jumpInput;
    
    public void InitBlackboard()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        moveInput = GetComponent<PlayerInput>().actions["Move"];
        jumpInput = GetComponent<PlayerInput>().actions["Jump"];
    }
}
