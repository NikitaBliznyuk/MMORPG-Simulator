using GameScreen.Character;
using GameScreen.Loader;
using GameScreen.UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace GameScreen.UI.View
{
	public class TopUiBehaviour : MonoBehaviour, IUiBehaviour
	{
		[Header("References")]
		
		[SerializeField] private Text nameText;
		[SerializeField] private BarBehaviour healthBar;
		[SerializeField] private Button closeButton;

		private void Awake()
		{
			Loader.Loader.DataUpdated += OnDataUpdated;
		}

		private void OnDataUpdated(LevelCurrentData data)
		{
			closeButton.onClick.AddListener(() =>
				data.PlayerReference.GetComponent<IInputController>().CurrentObservableInfo = null);
		}

		public void UpdateInfo(StatsInfo info)
		{
			if(info == null) return;
			
			healthBar.UpdateBarInfo((int) info.MaxHealth, (int) info.CurrentHealth);
			nameText.text = info.Name;
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}
	}
}
