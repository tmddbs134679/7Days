using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
    private readonly int IdleHas = Animator.StringToHash("Idle");
    private const float CrossFadeDuration = 0.1f;

    public AIIdleState(GameObject owner) : base(owner)
    {
        owner.GetComponent<Animator>().CrossFadeInFixedTime(IdleHas, CrossFadeDuration);
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
