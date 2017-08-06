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
        private IInputController inputController;

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
            inputController = playerInfo.GetComponent<IInputController>();
        }

        private void Start()
        {
            topUI.SetActive(false);
        }

        public void UpdateTopUi()
        {
            if (topUI == null)
            {
                Debug.LogWarning("There is no top ui on scene, try to switch scene or add one.");
                return;
            }

            bool active = inputController.CurrentObservableInfo != null;
            
            if (active)
            {
                StatsInfo info = inputController.CurrentObservableInfo.Info.StatsInfo;
                topUI.UpdateInfo(info);
            }
            
            topUI.SetActive(active);
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
            UpdateTopUi();
            
            if(heroUI != null && playerInfo != null)
                UpdateHeroUI();
        }
    }
}
