using System;
using UnityEngine;

namespace Game.Character
{
    [Serializable]
    public class CharacterInfo
    {
        public StatsInfo StatsInfo;
        public float MovementSpeed;
        public Ability[] Abilities;
        public string Tag;
        public string[] AllyTags;
        public string[] EnemyTags;
    }
    
    [Serializable]
    public class StatsInfo
    {
        [Header("Health")]
        
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        [SerializeField] private float healthRegen;
        
        [Header("Energy")]
        
        [SerializeField] private float maxEnergy;
        [SerializeField] private float currentEnergy;
        [SerializeField] private float energyRegen;

        [Header("Other")]
        
        [SerializeField] private string name;

        public StatsInfo(float maxHealth, float currentHealth, float healthRegen, float maxEnergy, float currentEnergy,
            float energyRegen, string name)
        {
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
            this.healthRegen = healthRegen;
            this.maxEnergy = maxEnergy;
            this.currentEnergy = currentEnergy;
            this.energyRegen = energyRegen;
            this.name = name;
        }

        public float MaxHealth
        {
            get { return maxHealth; }
        }

        public float CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                currentHealth = Mathf.Clamp(value, 0.0f, maxHealth);
            }
        }

        public float HealthRegen
        {
            get { return healthRegen; }
        }

        public float MaxEnergy
        {
            get { return maxEnergy; }
        }

        public float CurrentEnergy
        {
            get { return currentEnergy; }
            set { currentEnergy = Mathf.Clamp(value, 0.0f, maxEnergy); }
        }

        public float EnergyRegen
        {
            get { return energyRegen; }
        }

        public string Name
        {
            get { return name; }
        }
    }
}

