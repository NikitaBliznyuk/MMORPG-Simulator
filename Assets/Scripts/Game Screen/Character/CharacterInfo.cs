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

        public float MovementSpeed
        {
            get { return movementSpeed; }
        }

        [SerializeField] private UiInfo statsInfo;
        [SerializeField] private float movementSpeed;
    }
}

