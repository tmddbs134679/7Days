using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Transition
{
    public EnemyState FromState { get; }
    public EnemyState ToState { get; }
    public Func<bool> Condition { get; }

    public Transition(EnemyState from, EnemyState to, Func<bool> condition)
    {
        FromState = from;
        ToState = to;
        Condition = condition;
    }
}