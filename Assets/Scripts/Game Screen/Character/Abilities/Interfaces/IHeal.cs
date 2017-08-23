using System;

namespace GameScreen.Character.Abilities
{
    /// <summary>
    /// Interface for healing abilities.
    /// </summary>
    public interface IHeal
    {
        /// <summary>
        /// Reference to heal info.
        /// </summary>
        HealInfo HealInfo { get; }
    }

    /// <summary>
    /// This struct contains main heal info.
    /// </summary>
    [Serializable]
    public struct HealInfo
    {
        /// <summary>
        /// Minimum healing value.
        /// </summary>
        public int MinHeal;

        /// <summary>
        /// Maximum healing value.
        /// </summary>
        public int MaxHeal;
    }
}