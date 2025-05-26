using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyState
{
    private Transform target;
    private float speed;
    public EnemyChasingState(GameObject owner, Transform target, float speed) : base(owner)
    {
        this.target = target;
        this.speed = speed;
    }

    public override void Enter()
    {
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
       
    }

}
