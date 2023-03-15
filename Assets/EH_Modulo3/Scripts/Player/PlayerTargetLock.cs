using SCPE;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class PlayerTargetLock : MonoBehaviour
{
    public bool IsTargetLocked;

    [Header("Target Lock Detection")]
    [SerializeField] private float radius = 5f;

    [Header("Camera Animator")]
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private string normalState = "PlayerCamera";
    [SerializeField] private string lockOnState = "LockOnCamera";

    [Header("Post Processing")]
    [SerializeField] private Volume postProcessingVolume;

    private BlackBars blackBars;

    private EnemyController lockedEnemy;

    private void Awake()
    {
        postProcessingVolume.profile.TryGet(out blackBars);
    }

    public void UpdateLock()
    {
        if (InputManager.TARGETLOCK)
        {
            HandleTargetLockActivation();

        }

        if (IsTargetLocked)
        {
            Vector3 newTarget = lockedEnemy.transform.position;
            newTarget.y = transform.position.y;
            transform.LookAt(newTarget);
        }
    }

    float blackBarsAmount = 0;

    private void HandleTargetLockActivation()
    {
        IsTargetLocked = !IsTargetLocked;

        if (IsTargetLocked)
        {
            lockedEnemy = GetNearestEnemy();

            if (lockedEnemy == null)
            {
                IsTargetLocked = false;
                return;
            }

            cameraAnimator.Play(lockOnState);

            DOTween.To(() => blackBarsAmount, x => blackBarsAmount = x, 1, 0.25f).OnUpdate(() =>
            {
                //Debug.Log(")
                blackBars.size.value = blackBarsAmount;
            });

            lockedEnemy.ActivateUI();
            
        }
        else
        {
            cameraAnimator.Play(normalState);

            DOTween.To(() => blackBarsAmount, x => blackBarsAmount = x, 0, 0.25f).OnUpdate(() =>
            {
                blackBars.size.value = blackBarsAmount;
            });

            lockedEnemy.DeactivateUI();
            lockedEnemy = null;
        }
    }

    private EnemyController GetNearestEnemy()
    {
        Collider[] allDetectedColliders = Physics.OverlapSphere(transform.position, radius);

        List<EnemyController> detectedEnemies = new List<EnemyController>();
        detectedEnemies.Clear();

        for (int i = 0; i < allDetectedColliders.Length; i++)
        {
            if (allDetectedColliders[i].GetComponent<EnemyController>())
            {
                detectedEnemies.Add(allDetectedColliders[i].GetComponent<EnemyController>());
            }
        }

        float minDistance = float.MaxValue;
        EnemyController selectedController = null;
        foreach(var enemy in detectedEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                selectedController = enemy;
                minDistance = distance;
            }
        }

        Debug.Log($"Target locked on {selectedController.name}!");
        return selectedController;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
