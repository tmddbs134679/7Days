using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Stealth : AI_Base
{
    public float StealthTime = 5f;
    public float StealthRange = 10f;
    public float StealthDurtaion = 5f;

    void Start()
    {
       // Setting();

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
        fsm.AddTransition(chase, idle, () => Vector3.Distance(transform.position, player.transform.position) > enemyData.chasingRange);
        fsm.AddTransition(chase, attack, () => Vector3.Distance(transform.position, player.transform.position) < enemyData.attackRange);
        fsm.AddTransition(attack, chase, () => Vector3.Distance(transform.position, player.transform.position) > enemyData.attackRange);
    }
}
