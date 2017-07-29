using UnityEngine;

namespace Game.UI.View
{
	public class HeroUiBehaviour : MonoBehaviour, IUiBehaviour
	{
		[Header("References")]
		
		[SerializeField] private BarBehaviour healthBar;
		[SerializeField] private BarBehaviour energyBar;

		public void UpdateInfo(UiInfo info)
		{
			if(info == null) return;
			
			healthBar.UpdateBarInfo(info.MaxHealth, info.CurrentHealth);
			energyBar.UpdateBarInfo(info.MaxEnergy, info.CurrentEnergy);
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}