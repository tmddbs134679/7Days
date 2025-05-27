using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AIState
{
    private GameObject target;

    public GameObject CurrentTarget => target;
    public AIAttackState(GameObject owner) : base(owner)
    {
    }

    public override void Enter()
    {
        Debug.Log("Attack");
        SetTarget(target);
    }
    public override void Tick()
    {

    }

    public override void Exit()
    {
       
    }
    public void SetTarget(GameObject t)
    {
        target = t;
    }



}
