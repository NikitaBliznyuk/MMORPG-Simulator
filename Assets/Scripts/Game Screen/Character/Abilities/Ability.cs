using System;
using UnityEngine;

namespace GameScreen.Character.Abilities
{
    /// <summary>
    /// Generic class for all abilities.
    /// </summary>
    public abstract class Ability : ScriptableObject
    {
        [SerializeField]
        [Tooltip("Ability information.")]
        private AbilityInfo abilityInfo;

        /// <summary>
        /// Reference to ability information (read + write).
        /// </summary>
        public AbilityInfo AbilityInfo
        {
            get { return abilityInfo; }
            set { abilityInfo = value; }
        }

        /// <summary>
        /// Abstract method for all abilities.
        /// </summary>
        /// <param name="invoker">Invoker.</param>
        /// <param name="target">Target.</param>
        /// <returns>Ability invoke code. <seealso cref="AbilityInvokeErrorCode"/>></returns>
        public abstract AbilityInvokeErrorCode Invoke(CharacterInfoController invoker, CharacterInfoController target);
    }

    /// <summary>
    /// Ability information.
    /// </summary>
    [Serializable]
    public struct AbilityInfo
    {
        /// <summary>
        /// Ability name.
        /// </summary>
        public string Name;

        /// <summary>
        /// Ability desctition. Few sentencies.
        /// </summary>
        public string Description;

        /// <summary>
        /// Ability cooldown in seconds.
        /// </summary>
        public float Cooldown;

        /// <summary>
        /// Ability energy cost.
        /// </summary>
        public int Cost;

        /// <summary>
        /// Ability cast distance in Unity units.
        /// </summary>
        public float CastDistance;
    }

    public enum AbilityInvokeErrorCode
    {
        NO_ERROR,
        TOO_FAR,
        NO_ENERGY,
        NO_SUCH_ABILITY,
        NOT_AVALIABLE,
        WRONG_TARGET
    }
}