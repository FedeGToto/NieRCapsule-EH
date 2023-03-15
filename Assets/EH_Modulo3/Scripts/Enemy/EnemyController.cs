using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour, IDamagable
{
    [SerializeField] private float health;

    [Header("Bullet Manager")]
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private EnemyBulletSpawner bulletSpawner;

    [Header("UI")]
    [SerializeField] private GameObject ui;
    [SerializeField] private HealthBar healthBar;

    private float maxHealth;

    private StateMachine<EnemyController> stateMachine;
    public StateMachine<EnemyController> StateMachine { get { return stateMachine; } }

    public event UnityAction<float> OnDamageTake; 
    public event UnityAction OnDie;

    private void Awake()
    {
        stateMachine = new StateMachine<EnemyController>(this);
    }

    private void Start()
    {
        maxHealth = health;
        healthBar.SetupBar(this);

        stateMachine.ChangeState(new EnemyIdle());
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        OnDamageTake?.Invoke(dmg);

        if (health < 0)
        {
            health = 0;
            OnDie?.Invoke();
        }
    }

    public void ActivateUI() => ui.SetActive(true);
    public void DeactivateUI() => ui.SetActive(false);

    public BulletManager GetBulletManager() => bulletManager;
    public EnemyBulletSpawner GetBulletSpawner() => bulletSpawner;

    public float GetHealth() => health;

    public float GetMaxHealth() => maxHealth;
}
