using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Blackboard_Player))]
public class Player : Entity
{
    protected override StaterType EntityStaterType => StaterType.Player;
}
