using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorStateHit : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerController>().SetState(PlayerState.Idle);
    }

    
}
