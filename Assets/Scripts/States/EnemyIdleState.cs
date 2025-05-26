using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(GameObject owner) : base(owner)
    {
    }

    public override void Enter()
    {
        Debug.Log("Idle");
    }

    public override void Exit()
    {
     
    }

    public override void Tick()
    {
      
    }

 
}
