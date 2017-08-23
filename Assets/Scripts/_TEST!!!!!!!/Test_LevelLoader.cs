using GameScreen.Level;
using GameScreen.Loader;
using UnityEngine;

namespace IngameTests
{
	public class Test_LevelLoader : MonoBehaviour
	{
		[SerializeField] private LevelData data;

		private void Start()
		{
			data.Player.CharacterInfo.StatsInfo.CurrentHealth = data.Player.CharacterInfo.StatsInfo.MaxHealth;
			data.Player.CharacterInfo.StatsInfo.CurrentEnergy = data.Player.CharacterInfo.StatsInfo.MaxEnergy;
			foreach (var playerAbility in data.Player.CharacterInfo.Abilities)
			{
				playerAbility.Avaliable = true;
			}
            
			Loader.StartLevel(data);
		}
	}
}
