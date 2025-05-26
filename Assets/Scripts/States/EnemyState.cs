using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class EnemyState
{

    protected GameObject owner;

    public EnemyState(GameObject owner)
    {
        this.owner = owner;
    }


    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();
}
