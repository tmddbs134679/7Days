using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class AI_Runner : AI_Base
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



        fsm.AddTransition(chase, attack, () =>
        {
            var target = chase.CurrentTarget;
            if (target == null) return false;

            bool isWall = target.CompareTag("Wall");
            float dist = Vector3.Distance(transform.position, target.position);

            bool readyToAttack = dist < enemyData.attackRange;
            if (readyToAttack)
            {
                attack.SetTarget(chase.CurrentTarget.gameObject);
            }

            return dist < enemyData.attackRange;
        });

        fsm.AddTransition(attack, idle, () =>
        {
            var t = attack.CurrentTarget;
            return t == null || !t.gameObject.activeInHierarchy;
        });
        fsm.AddTransition(attack, chase, () => Vector3.Distance(transform.position, chase.CurrentTarget.position) < enemyData.chasingRange);
    }
}
