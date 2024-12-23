using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBirdState
{
    void EnterState(Bird bird);
    void UpdateState(Bird bird);
    void ExitState(Bird bird);
}

public class AppearState : IBirdState
{
    public void EnterState(Bird bird)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState(Bird bird)
    {
        throw new System.NotImplementedException();
    }

    public void ExitState(Bird bird)
    {
        throw new System.NotImplementedException();
    }
}

public class BirdState : MonoBehaviour
{
}
