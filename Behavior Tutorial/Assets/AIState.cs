using System;
using Unity.Behavior;

[BlackboardEnum]
public enum AIState
{
    Idle,
	Wander,
	Chase,
	Attack,
	Die
}
