using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject attacker { get; private set; }
   
    protected float damage;

    public void Init(GameObject attacker, float speed, float dmg)
    {
        this.attacker = attacker;
        damage = dmg;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
       // 충돌하면 타워 대미지 
    }
}
