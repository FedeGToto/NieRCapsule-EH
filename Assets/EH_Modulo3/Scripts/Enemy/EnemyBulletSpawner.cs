using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSpawner : MonoBehaviour
{
    [SerializeField, Min(1)] private int _numberOfProjectiles;
    [SerializeField, Min(0)] private float _projectileSpeed;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damage;
    [SerializeField, Range(0, 2)] private float _spawnRadius = 0.2f;

    public event EventHandler OnAttackFinished;

    private Vector3 _playerPosition;
    private EnemyController parent;

    private void Start()
    {
         parent = GetComponent<EnemyController>();
    }

    public void ResetPosition()
    {
        _playerPosition = transform.position;
    }

    public EnemyController GetParent() => parent;

    public void MakeAttack(ProjectileAttackTemplate shotData)
    {
        ResetPosition();
        StartCoroutine(Attack(shotData));
    }

    private IEnumerator Attack(ProjectileAttackTemplate shotData)
    {
        float maxAngle = shotData.MaxAngle;
        float angleStep = maxAngle / shotData.Number;
        float angle = 0f;
        float transformUpAngle = Mathf.Atan2(transform.up.x, transform.up.z);
        float PIx2 = Mathf.PI * 2;
        int currentRow = 0;
        bool rowsAdding = true;

        for (int r = 0; r < shotData.Repetition; r++)
        {
            for (int i = 0; i < shotData.Number; i++)
            {

                if (rowsAdding)
                {
                    if (currentRow >= shotData.Rows)
                    {
                        rowsAdding = false;
                    }
                    else
                    {
                        currentRow++;
                    }
                }
                else
                {
                    if (currentRow <= 0)
                    {
                        rowsAdding = true;
                    }
                    else
                    {
                        currentRow--;
                    }
                }


                Vector3 startPosition = new Vector3(
                    Mathf.Sin((angle * Mathf.PI / 180) + transformUpAngle),
                    0.5f + (shotData.VerticalAngle * currentRow),
                    Mathf.Cos((angle * Mathf.PI / 180) + transformUpAngle)) + shotData.Offset;

                Vector3 relativeStartPosition = _playerPosition + startPosition * shotData.Radius;

                float rotationZ = (maxAngle - angle) - (angle * PIx2 + transformUpAngle) * Mathf.Rad2Deg;
                Vector3 shotMovementVector = (relativeStartPosition -
                    (_playerPosition + new Vector3(0, 0.5f, 0) + shotData.Offset + Vector3.up * (shotData.VerticalAngle * currentRow))
                    ).normalized * shotData.Speed;

                Bullet bullet = parent.GetBulletManager().SpawnBullet(relativeStartPosition, Quaternion.Euler(0, 0, rotationZ), Vector3.one);
                bullet.Damage = damage;
                bullet.SetSpeed(shotMovementVector);

                angle += angleStep;

                if (shotData.Delay > 0)
                {
                    yield return new WaitForSeconds(shotData.Delay);
                }
            }

            if (shotData.Repetition > 1)
            {
                yield return new WaitForSeconds(shotData.RepetitionDelay);
            }
        }

        OnAttackFinished?.Invoke(this, EventArgs.Empty);
    }
}
