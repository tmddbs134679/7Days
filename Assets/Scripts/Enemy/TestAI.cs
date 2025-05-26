using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class TestAI : AIBase
{

    protected override void Awake()
    {
        base.Awake();
    }


    void Start()
    {
        var idle = new AIIdleState(gameObject);
        var chase = new AIChasingState(gameObject, player.transform, 3f);
        var attack = new AIAttackState(gameObject);

        fsm.SetInitialState(idle);

        fsm.AddTransition(idle, chase, () => Vector3.Distance(transform.position, player.transform.position) < 10f);
        fsm.AddTransition(chase, idle, () => Vector3.Distance(transform.position, player.transform.position) > 10f);
        fsm.AddTransition(chase, attack, () => Vector3.Distance(transform.position, player.transform.position) < attackRange);
        fsm.AddTransition(attack, chase, () => Vector3.Distance(transform.position, player.transform.position) > attackRange);

    }


    public override void Attack(GameObject target)
    {

    }

 
}
