using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth.OnHealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(object sender, float health)
    {
        SetHealth(health);
    }

    public void SetHealth(float health)
    {
        healthBarImage.fillAmount = health;
    }
}
