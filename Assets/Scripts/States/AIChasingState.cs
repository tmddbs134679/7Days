using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIChasingState : AIState
{
    private NavMeshAgent agent;
    private Transform target;

    public AIChasingState(GameObject owner, Transform target) : base(owner)
    {
        this.target = target;
        this.agent = owner.GetComponent<NavMeshAgent>();
  
    }

    public override void Enter()
    {
        agent.isStopped = false;
        Debug.Log("Chasing");
    }
    public override void Tick()
    {
        //owner.transform.position =
        //    Vector3.MoveTowards
        //    (
        //         owner.transform.position,
        //         target.position,
        //         speed * Time.deltaTime
        //    );
        agent.SetDestination(target.position);
      
    }

    public override void Exit()
    {
        //agent.isStopped = true;
    }

}
