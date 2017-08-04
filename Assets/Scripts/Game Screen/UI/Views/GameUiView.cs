﻿using System;
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
}
