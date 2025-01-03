using System;
using System.Collections.Generic;
public class StateTypesClasses
{
	public enum StateTypes
	{
		None,
		IdleState,
		JumpState,
		WalkState,
		Max
	}
	private static readonly Dictionary<Type, StateTypes> TypeToState = new()
	{
		[typeof(IdleState)] = StateTypes.IdleState,
		[typeof(JumpState)] = StateTypes.JumpState,
		[typeof(WalkState)] = StateTypes.WalkState,
	};
	public static StateTypes GetState<T>() => GetState(typeof(T));
	public static StateTypes GetState(Type type )
	{
		return TypeToState.GetValueOrDefault(type, StateTypes.None);
	}
}
