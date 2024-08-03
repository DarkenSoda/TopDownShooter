using System;
using UnityEngine;

namespace Game.FullGame
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [Header("Health")]
        [SerializeField] private int maxHealth = 100;
        private int currentHealth;

        public event EventHandler<float> OnHealthChanged;
        public event EventHandler OnDeath;
        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            OnHealthChanged?.Invoke(this, (float)currentHealth / maxHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                OnDeath?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Heal(int amount)
        {
            currentHealth += amount;

            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }
    }
}
