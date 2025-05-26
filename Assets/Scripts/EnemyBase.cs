using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField, Range(1,5)] protected float moveSpeed = 3;

    protected Animator animator;
    protected AudioSource audioSource;
    protected NavMeshAgent agent;
    EnemyStateMachine stateMachine;


    public abstract void Attack(GameObject target);
    protected virtual void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        animator =  GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();

        agent.speed = moveSpeed;
        agent.stoppingDistance = 0.05f;
        agent.radius = 0.1f;
      
    }

}
