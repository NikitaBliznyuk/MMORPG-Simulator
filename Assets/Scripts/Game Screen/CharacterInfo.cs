using Game.UI.View;
using UnityEngine;

namespace Game
{
    public class CharacterInfo : MonoBehaviour
    {
        public UiInfo StatsInfo
        {
            get { return statsInfo; }
        }

        [SerializeField] private UiInfo statsInfo;
    }
}

