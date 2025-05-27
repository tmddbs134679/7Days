using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Spine : AIBase
{
    void Start()
    {
        Setting();
    }





    public override void Attack(GameObject target)
    {

    }

    protected override void Setting()
    {
        var idle = new AIIdleState(gameObject);
        var chase = new AIChasingState(gameObject, player.transform);
        var attack = new AIAttackState(gameObject);
        var overWall = new AIOverWallState(gameObject, player.transform);

        fsm.SetInitialState(idle);

        fsm.AddTransition(idle, chase, () => Vector3.Distance(transform.position, player.transform.position) < chasingRange);
        fsm.AddTransition(chase, idle, () => Vector3.Distance(transform.position, player.transform.position) > chasingRange);
        fsm.AddTransition(idle, chase, () => Vector3.Distance(transform.position, player.transform.position) < attackRange);
        fsm.AddTransition(chase, overWall, () => Vector3.Distance(transform.position, player.transform.position) < attackRange);
        fsm.AddTransition(overWall, idle, () => overWall.IsClimbComplete);
        //fsm.AddTransition(attack, chase, () => Vector3.Distance(transform.position, player.transform.position) > attackRange);
    }
}
