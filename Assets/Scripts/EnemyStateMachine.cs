using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class EnemyStateMachine : MonoBehaviour
{
    public Dictionary<EENEMYSTATE, EnemyState> States = new Dictionary<EENEMYSTATE, EnemyState>();

    public EnemyState CurrentState { get; private set; }
    private void Update()
    {
        CurrentState?.Tick();
    }

    public void SwitchState(EnemyState newState)
    {
        if (CurrentState == newState)
            return;


        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    protected virtual void Awake()
    {
        //States.Add(EENEMYSTATE.IDLE, new EnemyIdleState(this));
        //States.Add(EENEMYSTATE.ATTACK, new EnemyAttackState(this));
        //States.Add(EENEMYSTATE.CHASING, new EnemyDeadState(this));
        //States.Add(EENEMYSTATE.DIE, new EnemyStunState(this));

        SwitchState(States[EENEMYSTATE.IDLE]);
    }

}
