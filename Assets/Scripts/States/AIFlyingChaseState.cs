using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlyingChaseState : AIState
{

    public Transform CurrentTarget { get; set; }
    private float wallDetectDistance;
    private float speed;


    public AIFlyingChaseState(GameObject owner, Transform target) : base(owner)
    {
        CurrentTarget = target;
    }

    public override void Enter()
    {
        Debug.Log("FlyChasing");
        AI_Base ai = owner.GetComponent<AI_Base>();
        speed = ai.enemyData.moveSpeed;
        wallDetectDistance = ai.enemyData.wallDetectDistance;
    }

    public override void Tick()
    {
        if (CurrentTarget == null)
            return;

        Vector3 targetPos = CurrentTarget.position;
        targetPos.y = owner.transform.position.y; // Y축 고정

        Vector3 dir = (targetPos - owner.transform.position).normalized;


        CurrentTarget = SelectTarget();

        owner.transform.position += dir * speed * Time.deltaTime;

        //if (IsWallInFront(out Transform wall))
        //{
        //    if (CurrentTarget != wall)
        //    {
        //        CurrentTarget = wall;
        //    }
        //    return;
        //}
    }

    public override void Exit()
    {
       
    }
    Transform SelectTarget()
    {

        // 플레이어 기본 타겟
        Transform player = TestGameManager.Inst.testPlayer.transform;

        if (IsWallInFront(out Transform wall))
        {
            return wall;
        }

        return player;
    }
    bool IsWallInFront(out Transform wall)
    {
        Vector3 origin = owner.transform.position + Vector3.up * 0.5f;
        Vector3 dir = owner.transform.forward;

        int wallLayerMask = LayerMask.GetMask("Wall");

        Collider[] hits = Physics.OverlapSphere(origin, wallDetectDistance, wallLayerMask);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Wall"))
            {
                wall = hit.transform;
                return true;
            }
        }

        wall = null;
        return false;
    }
}
