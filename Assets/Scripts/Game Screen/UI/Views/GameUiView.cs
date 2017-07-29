using System;
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

        public void UpdateHeroUI(UiInfo info, bool active)
        {
            if (heroUI == null)
            {
                Debug.LogWarning("There is no hero ui on scene, try to switch scene or add one.");
                return;
            }
            heroUI.SetActive(active);
            heroUI.UpdateInfo(info);
        }

        private void Update()
        {
            if(currentObservableInfo != null)
                UpdateTopUi(currentObservableInfo, true);
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
