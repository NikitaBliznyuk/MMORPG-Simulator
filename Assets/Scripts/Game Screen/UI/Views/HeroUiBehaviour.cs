using UnityEngine;

namespace Game.UI.View
{
	public class HeroUiBehaviour : MonoBehaviour, IUiBehaviour
	{
		[Header("References")]
		
		[SerializeField] private BarBehaviour healthBar;
		[SerializeField] private BarBehaviour energyBar;

		public void UpdateInfo(StatsInfo info)
		{
			if (info == null) return;

			healthBar.UpdateBarInfo((int) info.MaxHealth, (int) info.CurrentHealth);
			energyBar.UpdateBarInfo((int) info.MaxEnergy, (int) info.CurrentEnergy);
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}