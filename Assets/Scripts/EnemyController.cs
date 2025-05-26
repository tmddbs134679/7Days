using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject testplayer;
    private EnemyBase monster;
    protected EnemyStateMachine fsm = new();
    // Start is called before the first frame update

    private void Awake()
    {
        monster = GetComponent<EnemyBase>();
    }
    void Start()
    {
        var idle = new EnemyIdleState(gameObject);
        var chase = new EnemyChasingState(gameObject, testplayer.transform, 3f);
        var attack = new EnemyAttackState(gameObject);

        fsm.SetInitialState(idle);

        fsm.AddTransition(idle, chase, () => Vector3.Distance(transform.position, testplayer.transform.position) < 10f);
        fsm.AddTransition(chase, idle, () => Vector3.Distance(transform.position, testplayer.transform.position) > 10f);

        fsm.AddTransition(chase, attack, () => Vector3.Distance(transform.position, testplayer.transform.position) < monster.attackRange);
        fsm.AddTransition(attack, chase, () =>Vector3.Distance(transform.position, testplayer.transform.position) > monster.attackRange);

    }

    // Update is called once per frame
    void Update()
    {
        fsm.Tick();
    }

 
}
