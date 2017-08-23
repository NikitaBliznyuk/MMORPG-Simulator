using System;

namespace GameScreen.Character.Abilities
{
    /// <summary>
    /// Interface for hit abilities.
    /// </summary>
    public interface IHit
    {
        /// <summary>
        /// Reference to hit info.
        /// </summary>
        HitInfo HitInfo { get; }
    }

    /// <summary>
    /// Contains main hit ability info.
    /// </summary>
    [Serializable]
    public struct HitInfo
    {
        public int MinDamage;
        public int MaxDamage;
    }
}