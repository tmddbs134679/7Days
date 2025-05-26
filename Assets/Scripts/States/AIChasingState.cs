using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIChasingState : AIState
{
    private NavMeshAgent agent;
    private Transform target;
    private float speed;

    public AIChasingState(GameObject owner, Transform target, float speed) : base(owner)
    {
        this.target = target;
        this.agent = owner.GetComponent<NavMeshAgent>();
        this.speed = speed;
    }

    public override void Enter()
    {
        agent.isStopped = false;
        Debug.Log("Chasing");
    }
    public override void Tick()
    {
        owner.transform.position =
            Vector3.MoveTowards
            (
                 owner.transform.position,
                 target.position,
                 speed * Time.deltaTime
            );
      
    }

    public override void Exit()
    {
        //agent.isStopped = true;
    }

}
