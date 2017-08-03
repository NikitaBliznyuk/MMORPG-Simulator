using System;
using Game.Character;
using UnityEngine;

namespace Game.UI.View
{
    public class GameUiView : MonoBehaviour
    {
        [Header("References")]
        
        [SerializeField] private GameObject topUIReference;
        [SerializeField] private GameObject heroUIReference;
        
        private IUiBehaviour topUI;
        private IUiBehaviour heroUI;

        private CharacterInfoController playerInfo;

        private StatsInfo currentObservableInfo;

        private void Awake()
        {
            if(topUIReference != null)
                topUI = topUIReference.GetComponent<IUiBehaviour>();
            if(heroUIReference != null)
                heroUI = heroUIReference.GetComponent<IUiBehaviour>();
            
            Loader.DataUpdated += LoaderOnDataUpdated;
        }

        private void LoaderOnDataUpdated(LevelCurrentData data)
        {
            playerInfo = data.PlayerReference;
        }

        private void Start()
        {
            topUI.SetActive(false);
        }

        public void UpdateTopUi(StatsInfo info, bool active)
        {
            if (topUI == null)
            {
                Debug.LogWarning("There is no top ui on scene, try to switch scene or add one.");
                return;
            }
            currentObservableInfo = active ? info : null;
            topUI.SetActive(active);
            topUI.UpdateInfo(info);
        }

        private void UpdateHeroUI()
        {
            if (heroUI == null)
            {
                Debug.LogWarning("There is no hero ui on scene, try to switch scene or add one.");
                return;
            }
            
            heroUI.UpdateInfo(playerInfo.Info.StatsInfo);
        }

        private void Update()
        {
            if(currentObservableInfo != null)
                UpdateTopUi(currentObservableInfo, true);
            if(heroUI != null && playerInfo != null)
                UpdateHeroUI();
        }
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
