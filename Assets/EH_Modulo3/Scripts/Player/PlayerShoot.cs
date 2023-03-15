using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private BulletManager playerBulletManager;
    [SerializeField] private AudioSource shootAudioSource;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float fireRate = 0.1f;

    private float fireCooldown;

    public void UpdateShoot()
    {
        if (InputManager.FIRE && fireCooldown <= 0)
        {
            ShootBullet();
            fireCooldown = fireRate;
        }
        else
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    private void ShootBullet()
    {
        Quaternion spawnRotation = Quaternion.identity;

        spawnRotation = spawnPosition.rotation;

        shootAudioSource.Stop();
        shootAudioSource.Play();

        Bullet bullet = playerBulletManager.SpawnBullet(spawnPosition.position, spawnRotation, Vector3.one);
        bullet.Damage = 1;
    }
}
