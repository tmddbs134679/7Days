using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIBase : MonoBehaviour
{
    [SerializeField, Range(1,5)] protected float moveSpeed = 3;
    public float attackRange = 2;
    protected Animator animator;
    protected AudioSource audioSource;
    protected NavMeshAgent agent;

    protected AIStateMachine fsm = new();

    public GameObject player;

    public abstract void Attack(GameObject target);
    protected virtual void Awake()
    {
        animator =  GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();

        agent.speed = moveSpeed;
        agent.stoppingDistance = 0.05f;
        agent.radius = 0.1f;
    }

    protected virtual void Update()
    {
        fsm.Tick();
    }


}
