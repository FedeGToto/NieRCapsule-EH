using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDamagable
{
    public event UnityAction<float> OnDamageTake;
    public event UnityAction OnDie;

    public void TakeDamage(float dmg)
    {
        Debug.Log("You should not see this");
    }

    public float GetHealth();
    public float GetMaxHealth();
}
