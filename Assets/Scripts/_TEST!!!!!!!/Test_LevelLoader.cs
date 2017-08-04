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

			foreach (var enemy in data.Enemies)
			{
				enemy.CharacterInfo.StatsInfo.CurrentHealth = enemy.CharacterInfo.StatsInfo.MaxHealth;
				enemy.CharacterInfo.StatsInfo.CurrentEnergy = enemy.CharacterInfo.StatsInfo.MaxEnergy;
				foreach (var enemyAbility in enemy.CharacterInfo.Abilities)
				{
					enemyAbility.Avaliable = true;
				}
			}
            
			Loader.StartLevel(data);
		}
	}
}
