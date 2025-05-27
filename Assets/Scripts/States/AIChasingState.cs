using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class AIChasingState : AIState
{
    private NavMeshAgent agent;
    public Transform CurrentTarget { get; set; }
    private EENEMYTYPE type;
    private float wallDetectDistance;
    public AIChasingState(GameObject owner, Transform target) : base(owner)
    {
        SO_EnemyAI data = owner.GetComponent<AI_Base>().enemyData;
        type = data.type;

        if (type == EENEMYTYPE.RUNNER || type == EENEMYTYPE.SPINE)
        {
            wallDetectDistance = data.wallDetectDistance;
        }

       // this.target = target;
        this.agent = owner.GetComponent<NavMeshAgent>();

     
    }

    public override void Enter()
    {
        agent.isStopped = false;
        Debug.Log("Chasing");
    }
    public override void Tick()
    {
       DebugRangeLine();

        if ((type == EENEMYTYPE.RUNNER || type == EENEMYTYPE.SPINE) && IsWallInFront(out Transform wall))
        {
            if (CurrentTarget != wall)
            {
                CurrentTarget = wall;
                agent.SetDestination(wall.position);
                return;
            }
        }

        if (CurrentTarget == null || IsDead(CurrentTarget))
        {
            CurrentTarget = SelectTarget(type);
            if (CurrentTarget != null)
                agent.SetDestination(CurrentTarget.position);
        }

        if (CurrentTarget != null)
            agent.SetDestination(CurrentTarget.position);


    }

    public override void Exit()
    {
        //agent.isStopped = true;
    }
    Transform SelectTarget(EENEMYTYPE type)
    {

        // 플레이어 기본 타겟
        Transform player = TestGameManager.Inst.testPlayer.transform;

        if (type == EENEMYTYPE.RUNNER || type == EENEMYTYPE.SPINE)
        {
            if (IsWallInFront(out Transform wall))
            {
                return wall;
            }
        }
       
        if (type == EENEMYTYPE.STEALER)
        {
            if (DroneManager.HasAliveDrones)
                return DroneManager.ClosestDrone(owner.transform.position);

            Transform building = DroneManager.ClosestDroneBuilding(owner.transform.position);
            if (building != null)
                return building;
        }


        return player;
    }

    bool IsWallInFront(out Transform wall)
    {
        Vector3 origin = owner.transform.position + Vector3.up * 0.5f;
        Vector3 dir = owner.transform.forward;

        int wallLayerMask = LayerMask.GetMask("Wall");
        if (Physics.Raycast(origin, dir, out RaycastHit hit, wallDetectDistance, wallLayerMask))
        {
            wall = hit.transform;
            return true;
        }

        wall = null;
        return false;
    }
    bool IsDead(Transform CurrentTarget)
    {
        return CurrentTarget == null || !CurrentTarget.gameObject.activeInHierarchy;
    }

    void DebugRangeLine()
    {
        Vector3 origin = owner.transform.position + Vector3.up * 0.5f;
        Vector3 dir = owner.transform.forward;
        Debug.DrawRay(origin, dir * wallDetectDistance, Color.red);
    }
}
