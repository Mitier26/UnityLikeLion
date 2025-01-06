using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFsm : MonoBehaviour
{
   private StateMachine stateMachine;
   [SerializeField] private StaterType stateType;

   private void Start()
   {
      stateMachine = GetComponent<StateMachine>();
      stateMachine.Run(stateType);
   }

   private void Update()
   {
       stateMachine.UpdateState();
   }
}
