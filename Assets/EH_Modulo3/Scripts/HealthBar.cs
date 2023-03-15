using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillBar;
    [SerializeField] private float barDuration = 0.1f;

    IDamagable parent;

    public void SetupBar(IDamagable parent)
    {
        this.parent = parent;

        parent.OnDamageTake += UpdateBar;
        UpdateBar(0);
    }

    private void OnDestroy()
    {
        parent.OnDamageTake -= UpdateBar;
    }

    public void UpdateBar(float dmg)
    {
        float fillAmount = parent.GetHealth() / parent.GetMaxHealth();

        fillBar.DOFillAmount(fillAmount, barDuration);
    }
}
