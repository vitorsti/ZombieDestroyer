using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    float health, maxHealth;
    float dif = 0;
    private void Awake()
    {
        health = maxHealth;
    }
    void SetHealth(float value)
    {
        health = value;
        if (health <= 0)
        {
            health = 0;
            Death();
        }

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public float GetHealth()
    {
        return health;
    }

    private void Death()
    {
#if UNITY_EDITOR
        Debug.Log("Dead");
#endif
    }

    public void DealDamage(float value)
    {
        dif = 0;
        dif = health;
        dif -= value;
        SetHealth(dif);
    }

    public void Heal(float value)
    {
        dif = 0;
        dif = health;
        dif += value;
        SetHealth(dif);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log(other.name);
        BulletBehavior bulletBehavior;
        bulletBehavior = other.GetComponent<BulletBehavior>();
        if (bulletBehavior != null)
        {
            DealDamage(other.GetComponent<BulletBehavior>().GetDamage());
        }
        ///}
    }*/
}
