using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int maxBullets = 300;

    private List<Bullet> activeBullets;
    private List<Bullet> inactiveBullets;

    private void Start()
    {
        activeBullets = new List<Bullet>();
        inactiveBullets = new List<Bullet>();

        Vector3 spawnPosition = new(0, -100, 0);

        for (int i = 0; i < maxBullets; i++)
        {
            Bullet spawnedBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            spawnedBullet.transform.parent = transform;
            spawnedBullet.SpawnBullet(this);
            inactiveBullets.Add(spawnedBullet);
        }
    }

    private void Update()
    {
        for (int i = 0; i < activeBullets.Count; i++)
        {
            activeBullets[i].UpdateBullet();
        }
    }

    public Bullet SpawnBullet(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        if (inactiveBullets.Count <= 0)
        {
            Debug.LogError("There are too many bullets. Wait for some to despawn.");
            return null;
        }

        Bullet refBullet = inactiveBullets[0];

        activeBullets.Add(refBullet);

        refBullet.transform.SetPositionAndRotation(position, rotation);
        refBullet.transform.localScale = scale;

        inactiveBullets.Remove(refBullet);

        return refBullet;
    }

    public void DestroyBullet(Bullet bullet)
    {
        if (activeBullets.Count <= 0)
        {
            Debug.LogError("I cannot destroy any bullet, there are any!");
            return;
        }

        if (!activeBullets.Contains(bullet))
        {
            Debug.LogError("This bullet is not spawned!");
            return;
        }

        bullet.transform.position = new(0, -100, 0);

        activeBullets.Remove(bullet);
        inactiveBullets.Add(bullet);
    }
}
