using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public struct ProjectileAttackTemplate
{
    public GameObject Prefab => _projectilePrefab;
    public float RepetitionDelay => _repetitionDelay;
    public float Repetition => _repetition;
    public Vector3 Offset => _offset;
    public int Number => _numberOfProjectiles;
    public float Speed => _projectileSpeed;
    public float Radius => _spawnRadius;
    public float Delay => _delay;
    public float MaxAngle => _maxAngle;
    public float VerticalAngle => _verticalAngle;
    public int Rows => _rows;


    private GameObject _projectilePrefab;
    private float _repetition;
    private Vector3 _offset;
    private int _numberOfProjectiles;
    private float _projectileSpeed;
    private float _spawnRadius;
    private float _delay;
    private float _repetitionDelay;
    private float _maxAngle;
    private float _verticalAngle;
    private int _rows;

    public ProjectileAttackTemplate(GameObject projectilePrefab, 
        float repetitionDelay, float repetition,
        Vector3 offset,
        int numberOfProjectiles = 1, float projetileSpeed = 15f,
        float spawnRadius = 0.1f, float delay = 0.1f, 
        float maxAngle = 360, float verticalAngle = 0, int rows = 1)
    {
        _projectilePrefab = projectilePrefab;
        _repetitionDelay = repetitionDelay;
        _repetition = repetition;
        _offset = offset;
        _numberOfProjectiles = numberOfProjectiles;
        _projectileSpeed = projetileSpeed;
        _spawnRadius = spawnRadius;
        _delay = delay;
        _maxAngle = maxAngle;
        _verticalAngle = verticalAngle;
        _rows = rows;
    }
}
