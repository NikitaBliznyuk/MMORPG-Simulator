using UnityEngine;

namespace Game.View
{
	public class HeroUiBehaviour : MonoBehaviour, IUiBehaviour
	{
		[Header("References")]
		
		[SerializeField] private BarBehaviour healthBar;
		[SerializeField] private BarBehaviour energyBar;

		public void UpdateInfo(UiInfo info)
		{
			healthBar.UpdateBarInfo(info.MaxHealth, info.CurrentHealth);
			energyBar.UpdateBarInfo(info.MaxEnergy, info.CurrentEnergy);
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}