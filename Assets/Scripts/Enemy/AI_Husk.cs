using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Husk : AI_Base
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

        fsm.SetInitialState(idle);

        fsm.AddTransition(idle, chase, () => Vector3.Distance(transform.position, player.transform.position) < enemyData.chasingRange);
        fsm.AddTransition(idle, chase, () =>
        {
            if (DroneManager.HasAliveDrones)
                return true;

            return Vector3.Distance(transform.position, player.transform.position) < enemyData.chasingRange;
        });
    }

}
