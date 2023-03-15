using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AttackLibrary
{
    private static GameObject _defaultProjectilePrefab;

    public static void Initialize(GameObject defaultProjectilePrefab)
    {
        _defaultProjectilePrefab = defaultProjectilePrefab;
    }
    // 20 bullet at once for 10 times
    public static ProjectileAttackTemplate Boss1Attack1() => new ProjectileAttackTemplate(
        projectilePrefab: _defaultProjectilePrefab,
        repetitionDelay: 1,
        repetition: 10,
        offset: Vector3.zero,
        numberOfProjectiles: 20,
        projetileSpeed: 10.0f,
        spawnRadius: 1f,
        delay: 0f,
        maxAngle: 360.0f,
        verticalAngle: 0.0f,
        rows: 1
        );

    // 45 angle spawn bullet radius
    public static ProjectileAttackTemplate Boss1Attack2() => new ProjectileAttackTemplate(
        projectilePrefab: _defaultProjectilePrefab,
        repetitionDelay: 1,
        repetition: 8,
        offset: Vector3.zero,
        numberOfProjectiles: 10,
        projetileSpeed: 10.0f,
        spawnRadius: 1f,
        delay: 0f,
        maxAngle: 45f,
        verticalAngle: 0.0f,
        rows: 1
        );

    // ??
    public static ProjectileAttackTemplate Boss1Attack3() => new ProjectileAttackTemplate(
        projectilePrefab: _defaultProjectilePrefab,
        repetitionDelay: 0,
        repetition: 20,
        offset: Vector3.zero,
        numberOfProjectiles: 20,
        projetileSpeed: 10.0f,
        spawnRadius: 1f,
        delay: 0.05f,
        maxAngle: 360.0f,
        verticalAngle: 0.7f,
        rows: 3
        );

    // burst of lots of bullets
    public static ProjectileAttackTemplate Boss1Attack4() => new ProjectileAttackTemplate(
        projectilePrefab: _defaultProjectilePrefab,
        repetitionDelay: 0.75f,
        repetition: 10,
        offset: new Vector3(0, 0.75f, 0),
        numberOfProjectiles: 100,
        projetileSpeed: 10.0f,
        spawnRadius: 1f,
        delay: 0f,
        maxAngle: 360.0f,
        verticalAngle: 0.0f,
        rows: 1
        );

    
    // Second Phase

    // Crown bullets
    public static ProjectileAttackTemplate Boss1Attack5() => new ProjectileAttackTemplate(
        projectilePrefab: _defaultProjectilePrefab,
        repetitionDelay: 0,
        repetition: 30,
        offset: Vector3.zero,
        numberOfProjectiles: 30,
        projetileSpeed: 10.0f,
        spawnRadius: 1f,
        delay: 0.01f,
        maxAngle: 360.0f,
        verticalAngle: 0.7f,
        rows: 3
        );

    public static ProjectileAttackTemplate Boss1Attack6() => new ProjectileAttackTemplate(
        projectilePrefab: _defaultProjectilePrefab,
        repetitionDelay: 0.2f,
        repetition: 15,
        offset: new Vector3(0, 1f, 0),
        numberOfProjectiles: 50,
        projetileSpeed: 10.0f,
        spawnRadius: 1f,
        delay: 0.015f,
        maxAngle: 360.0f,
        verticalAngle: 0.0f,
        rows: 1
        );


}
