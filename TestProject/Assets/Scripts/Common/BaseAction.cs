using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    public abstract bool RegistAction();
    
    public abstract bool UnRegistAction();
}
