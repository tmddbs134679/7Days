using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Spine : AI_Base
{
    [SerializeField] private GameObject projectilePrefab;
    private float startY = 9f;
    protected override void Start()
    {
        base.Start();
        Setting();
    }

    public override void Init()
    {
        Vector3 pos = transform.position;
        pos.y = startY;
        transform.position = pos;
    }

    public override void Attack(GameObject target)
    {
        if (projectilePrefab == null || target == null)
            return;

        Vector3 spawnPos = transform.position + Vector3.back * 4f;
        Vector3 targetPos = target.transform.position;

        Vector3 direction = (targetPos - spawnPos).normalized;
        float distance = Vector3.Distance(spawnPos, targetPos);
        float duration = 2f;
        float speed = distance / duration;

        GameObject proj = GameObject.Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(direction));
        proj.GetComponent<Projectile>().Init(gameObject);

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        rb.velocity = direction * speed;
    }

    protected override void Setting()
    {
        var idle = new AIIdleState(gameObject);
        var chase = new AIFlyingChaseState(gameObject, TestGameManager.Inst.testPlayer.transform);
        var attack = new AIAttackState(gameObject);
        
        var dead = new AIDeadState(gameObject);

        fsm.SetInitialState(idle);

        fsm.AddTransition(idle, chase, () => Vector3.Distance(transform.position, TestGameManager.Inst.testPlayer.transform.position) < enemyData.chasingRange);
        fsm.AddTransition(chase, attack, () =>
        {
            var target = chase.CurrentTarget;
            if (target == null) return false;

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
            return t == null || !t.activeInHierarchy;
        });

        fsm.AddAnyTransition(dead, () => GetComponent<Health>().IsDead);

    }
}
