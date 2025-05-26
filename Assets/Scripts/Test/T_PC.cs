using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_PC : MonoBehaviour
{
    public static T_PC instance;
    public event Action<float,float> OnHealthChanged;
    public event Action<float,float> OnStaminaChanged;



    private void Awake()
    {
        // 싱글톤
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    public float _maxHealth;
    public float _health;
    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            OnHealthChanged?.Invoke(_maxHealth,_health);
        }
    }
    public float _maxStamina;
    public float _stamins;
    public float Stamina
    {
        get => _stamins;
        set
        {
            _stamins = value;
            OnStaminaChanged?.Invoke(_maxStamina, _stamins);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Stamina -= 10;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Stamina += 10;
        }
    }
}
