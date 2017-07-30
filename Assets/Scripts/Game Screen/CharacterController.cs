using UnityEngine;

namespace Game
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private CharacterInfo info;

        public void DealDamage(int damage)
        {
            damage = damage >= 0 ? damage : 0;
            info.StatsInfo.CurrentHealth -= damage;
            info.StatsInfo.CurrentHealth = info.StatsInfo.CurrentHealth >= 0 ? info.StatsInfo.CurrentHealth : 0;
        }

        public void Heal(int value)
        {
            value = value >= 0 ? value : 0;
            info.StatsInfo.CurrentHealth += value;
            info.StatsInfo.CurrentHealth = info.StatsInfo.CurrentHealth <= info.StatsInfo.MaxHealth
                ? info.StatsInfo.CurrentHealth
                : info.StatsInfo.MaxHealth;
        }
    }
}
