using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private ICubeState currentState;
    
    public TMP_Text stateText;
    private Renderer cubeRenderer;

    private void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        ChangeState(new IdleState());
    }

    private void Update()
    {
        currentState?.UpdateState(this);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeState(new IdleState());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState(new MoveingState());
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState(new ScalingState());
        }
    }

    public void ChangeState(ICubeState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState?.EnterState(this);
    }

    public void SetColor(Color color)
    {
        cubeRenderer.material.color = color;
    }

    public void SetStateText(string text)
    {
        if (stateText != null)
        {
            stateText.text = text;
        }
    }
}
