using Game.UI.View;
using UnityEngine;

namespace Game.Character
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

