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
        var chase = new AIChasingState(gameObject, TestGameManager.Inst.testPlayer.transform);
        var attack = new AIAttackState(gameObject);

        fsm.SetInitialState(idle);

        fsm.AddTransition(idle, chase, () => Vector3.Distance(transform.position, TestGameManager.Inst.testPlayer.transform.position) < enemyData.chasingRange);
        fsm.AddTransition(idle, chase, () =>
        {
            if (DroneManager.HasAliveDrones)
                return true;

            return Vector3.Distance(transform.position, TestGameManager.Inst.testPlayer.transform.position) < enemyData.chasingRange;
        });
    }

}
