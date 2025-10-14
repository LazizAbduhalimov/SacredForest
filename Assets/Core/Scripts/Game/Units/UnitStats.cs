using System;
using UnityEngine;

namespace Client.Game
{
    [Serializable]
    public class UnitStats
    {
        public int CurrentHealth;
        public int InitialHealth;
        public int MaxHealth;
        
        public int Strength;
        public int Agility;
        public int Stamina;
        
        public Action OnDeath;
        public Action<int, int> OnHealthChanged;
        public Action<int, int, int> OnStatsChanged;

        public UnitStats(int health, int strength, int  agility, int stamina)
        {
            InitialHealth = health;
            Strength = strength;
            Agility = agility;
            Stamina = stamina;
            
            CurrentHealth = InitialHealth + Strength;
            MaxHealth = CurrentHealth;
        }
        
        public void TakeDamage(int damage)
        {
            var oldHealth =  CurrentHealth;
            CurrentHealth -= damage;
            OnHealthChanged?.Invoke(oldHealth, CurrentHealth);
            
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnDeath?.Invoke();
            }
        }

        public void FullHeal()
        {
            var oldHealth = CurrentHealth;
            CurrentHealth = MaxHealth;
            OnHealthChanged?.Invoke(oldHealth, CurrentHealth);
        }

        public void SetStats(int? strength, int? agility, int? stamina)
        {
            if ((strength.HasValue || agility.HasValue || stamina.HasValue) == false) return;

            Strength = strength ?? Strength;
            Agility = agility ?? Agility;
            Stamina = stamina ?? Stamina;
            OnStatsChanged?.Invoke(Strength, Agility, Stamina);
        }

        public void AddMaxHealth(int maxHealth)
        {
            MaxHealth += maxHealth;
        }
    }
}