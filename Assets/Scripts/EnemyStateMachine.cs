using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class EnemyStateMachine : MonoBehaviour
{
   // public Dictionary<EENEMYSTATE, EnemyState> States = new Dictionary<EENEMYSTATE, EnemyState>();
    public EnemyState CurrentState { get; private set; }
    private List<Transition> transitions = new();


    public void SetInitialState(EnemyState state)
    {
        CurrentState = state;
        CurrentState.Enter();
    }

    public void AddTransition(EnemyState from, EnemyState to, Func<bool> condition)
    {
        transitions.Add(new Transition(from, to, condition));
    }
    public void Tick()
    {
        foreach (var t in transitions)
        {
            if (t.FromState == CurrentState && t.Condition())
            {
                CurrentState.Exit();
                CurrentState = t.ToState;
                CurrentState.Enter();
                break;
            }
        }

        CurrentState?.Tick();

    }



}
