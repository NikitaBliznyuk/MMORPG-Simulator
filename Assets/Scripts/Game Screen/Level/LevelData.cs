﻿using System;
using UnityEngine;
using CharacterInfo = GameScreen.Character.CharacterInfo;

namespace GameScreen.Level
{
    [CreateAssetMenu(menuName = "Level Data")]
    public class LevelData : ScriptableObject
    {
        [SerializeField]
        private SpawnData player;

        [SerializeField]
        private SpawnData[] allies;

        public SpawnData Player
        {
            get { return player; }
        }

        public SpawnData[] Allies
        {
            get { return allies; }
        }
    }

    [Serializable]
    public struct SpawnData
    {
        public CharacterInfo CharacterInfo;
        public Sprite Icon;
    }
}
