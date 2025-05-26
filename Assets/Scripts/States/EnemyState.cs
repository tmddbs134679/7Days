using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class EnemyState : MonoBehaviour
{
    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();
}
