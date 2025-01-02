using System.Collections;
using UnityEngine;

public class MyCharacterControllerInterface : MonoBehaviour
{
    private StateMachine stateMachine;

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Run();
    }

    private void Update()
    {
        stateMachine.UpdateState();
    }
}
