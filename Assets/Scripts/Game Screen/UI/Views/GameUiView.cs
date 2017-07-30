using System;
using UnityEngine;
using CharacterInfo = Game.Character.CharacterInfo;

namespace Game.UI.View
{
    public class GameUiView : MonoBehaviour
    {
        [Header("References")]
        
        [SerializeField] private GameObject topUIReference;
        [SerializeField] private GameObject heroUIReference;
        [SerializeField] private CharacterInfo playerInfo;
        
        private IUiBehaviour topUI;
        private IUiBehaviour heroUI;

        private UiInfo currentObservableInfo;

        private void Awake()
        {
            if(topUIReference != null)
                topUI = topUIReference.GetComponent<IUiBehaviour>();
            if(heroUIReference != null)
                heroUI = heroUIReference.GetComponent<IUiBehaviour>();
        }

        private void Start()
        {
            topUI.SetActive(false);
        }

        public void UpdateTopUi(UiInfo info, bool active)
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
            
            heroUI.UpdateInfo(playerInfo.StatsInfo);
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
    public class UiInfo
    {
        public int MaxHealth;
        public int CurrentHealth;
        public int MaxEnergy;
        public int CurrentEnergy;
        public string Name;
    }
}
