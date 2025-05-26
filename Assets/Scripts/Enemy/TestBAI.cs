using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBAI : AIBase
{
    void Start()
    {
        var idle = new AIIdleState(gameObject);
        var chase = new AIChasingState(gameObject, player.transform, 3f);
        var attack = new AIAttackState(gameObject);
        var overWall = new AIOverWallState(gameObject, player.transform);

        fsm.SetInitialState(idle);

        fsm.AddTransition(idle, chase, () => Vector3.Distance(transform.position, player.transform.position) < 10f);
        fsm.AddTransition(chase, idle, () => Vector3.Distance(transform.position, player.transform.position) > 10f);
        fsm.AddTransition(idle, chase, () => Vector3.Distance(transform.position, player.transform.position) < attackRange);
        fsm.AddTransition(chase, overWall, () => Vector3.Distance(transform.position, player.transform.position) < attackRange);
        fsm.AddTransition(overWall, idle, () => overWall.IsClimbComplete);
        //fsm.AddTransition(attack, chase, () => Vector3.Distance(transform.position, player.transform.position) > attackRange);
    }





    public override void Attack(GameObject target)
    {

    }

}
