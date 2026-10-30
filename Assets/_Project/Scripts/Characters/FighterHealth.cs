using System;
using UnityEngine;

namespace LegendsOfTheArena.Characters
{
    public sealed class FighterHealth : MonoBehaviour
    {
        [SerializeField, Min(1)] private int maxHealth = 1000;
        public int CurrentHealth { get; private set; }
        public int MaxHealth => maxHealth;
        public bool IsKnockedOut => CurrentHealth <= 0;
        public event Action<int, int> HealthChanged;
        public event Action<int> Damaged;
        public event Action<FighterHealth> KnockedOut;

        private void Awake()
        {
            CurrentHealth = maxHealth;
            HealthChanged?.Invoke(CurrentHealth, maxHealth);
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0 || IsKnockedOut) return;
            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
            Damaged?.Invoke(amount);
            HealthChanged?.Invoke(CurrentHealth, maxHealth);
            if (IsKnockedOut) KnockedOut?.Invoke(this);
        }

        public void RestoreFullHealth()
        {
            CurrentHealth = maxHealth;
            HealthChanged?.Invoke(CurrentHealth, maxHealth);
        }
    }
}
