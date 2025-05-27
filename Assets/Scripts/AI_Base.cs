using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AI_Base : MonoBehaviour
{
    public SO_EnemyAI enemyData;
    protected Animator animator;
    protected AudioSource audioSource;
    protected NavMeshAgent agent;
    protected AIStateMachine fsm = new();
    public GameObject player;
    private Health health;


    public abstract void Attack(GameObject target);
    protected virtual void Awake()
    {
        health = GetComponent<Health>();
        animator =  GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyData.moveSpeed;

    }

    protected virtual void Start()
    {
        health.Init(enemyData.maxHealth);
    }

    protected virtual void Update()
    {
        fsm.Tick();
    }


    protected abstract void Setting();

     
}
