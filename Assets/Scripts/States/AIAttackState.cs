using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AIState
{
    public AIAttackState(GameObject owner) : base(owner)
    {
    }

    public override void Enter()
    {
        Debug.Log("Attack");
    }
    public override void Tick()
    {

    }

    public override void Exit()
    {
       
    }

 

 
}
