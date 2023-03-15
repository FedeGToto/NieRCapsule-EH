using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float Damage { get; set; }

    [SerializeField] private float speed;

    [SerializeField, Tooltip("In Seconds")] private float despawnTime = 5f;

    private BulletManager manager;
    private Rigidbody rb;
    private Vector3 speedVector;
    private bool isUsingSpeedVector;
    private float despawnTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SpawnBullet(BulletManager bulletManager)
    {
        this.manager = bulletManager;
        despawnTimer = despawnTime;
    }

    public void UpdateBullet()
    { 
        rb.velocity = isUsingSpeedVector ? speedVector : transform.forward * speed;

        if (despawnTimer <= 0)
        {
            DestroyBullet();
        }
        else
        {
            despawnTimer -= Time.deltaTime;
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
        isUsingSpeedVector = false;
    }

    public void SetSpeed(Vector3 speed)
    {
        this.speedVector = speed;
        isUsingSpeedVector = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground") || 
            (gameObject.CompareTag("Bullet") && collision.gameObject.CompareTag("Player")) ||
            (gameObject.CompareTag("PlayerBullet") && collision.gameObject.CompareTag("Enemy")))
        {
            if (collision.transform.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(Damage);
            }

            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        rb.velocity = Vector3.zero;
        despawnTimer = despawnTime;
        manager.DestroyBullet(this);
    }
}
