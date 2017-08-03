using System;
using Game.UI.View;

namespace Game.Character
{
    [Serializable]
    public class CharacterInfo
    {
        public UiInfo StatsInfo;
        public float MovementSpeed;
        public Ability[] Abilities;
        public string Tag;
        public string[] AllyTags;
        public string[] EnemyTags;
    }
}

