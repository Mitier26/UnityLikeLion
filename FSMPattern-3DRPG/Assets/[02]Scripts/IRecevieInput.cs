using System.Collections.Generic;
using UnityEngine;

public interface IRecevieInput
{   
    void OnTriggered(string action, bool triggerValue);
    void OnReadValue(string action, Vector2 value);
}