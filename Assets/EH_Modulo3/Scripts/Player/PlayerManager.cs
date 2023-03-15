using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using MoreMountains.Feedbacks;

public class PlayerManager : MonoBehaviour, IDamagable
{
    [Header("Stats")]
    public bool IsInvincible = false;
    [SerializeField] private float health = 100;
    private float maxHealth;

    [Header("UI")]
    [SerializeField] private HealthBar healthBar;

    [Header("VFX")]
    [SerializeField] private MMF_Player damageEffect;

    [Header("Features")]
    public bool CanMove = true;
    [SerializeField] private PlayerMovement playerMovement;
    public PlayerMovement GetMovement() => playerMovement;

    public bool CanLockTarget;
    [SerializeField] private PlayerTargetLock targetLock;

    public bool CanDash;
    [SerializeField] private PlayerDash playerDash;

    public bool CanShoot;
    [SerializeField] private PlayerShoot playerShoot;

    public event UnityAction<float> OnDamageTake;
    public event UnityAction OnDie;

    private void Awake()
    {
        maxHealth = health;

        healthBar.SetupBar(this);
    }

    private void Update()
    {
        if (CanMove)
            playerMovement.UpdateMovement();

        if (CanLockTarget)
            targetLock.UpdateLock();

        if (CanDash)
            playerDash.UpdateDash();

        if (CanShoot)
            playerShoot.UpdateShoot();
    }

    public void TakeDamage(float dmg)
    {
        if (IsInvincible)
            return;

        health -= dmg;
        OnDamageTake?.Invoke(dmg);
        damageEffect.PlayFeedbacks();

        if (health<= 0)
        {
            health = 0;
            OnDie?.Invoke();
        }
    }

    public float GetHealth() => health;
    public float GetMaxHealth() => maxHealth;
}
