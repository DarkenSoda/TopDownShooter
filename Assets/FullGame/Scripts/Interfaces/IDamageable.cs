using System;

public interface IDamageable
{
    public event EventHandler<float> OnHealthChanged;
    public event EventHandler OnDeath;

    void TakeDamage(int damage);
    void Heal(int amount);
}
