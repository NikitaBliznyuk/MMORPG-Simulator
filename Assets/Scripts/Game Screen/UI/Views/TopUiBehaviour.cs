using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.View
{
	public class TopUiBehaviour : MonoBehaviour, IUiBehaviour
	{
		[Header("References")]
		
		[SerializeField] private Text nameText;
		[SerializeField] private BarBehaviour healthBar;

		public void UpdateInfo(UiInfo info)
		{
			if(info == null) return;
			
			healthBar.UpdateBarInfo(info.MaxHealth, info.CurrentHealth);
			nameText.text = info.Name;
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}
