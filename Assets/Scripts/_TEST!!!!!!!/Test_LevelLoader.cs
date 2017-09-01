using GameScreen.Level;
using GameScreen.Loader;
using UnityEngine;
using CharacterInfo = GameScreen.Character.CharacterInfo;

namespace IngameTests
{
	public class Test_LevelLoader : MonoBehaviour
	{
		[SerializeField] private LevelData data;

		private void Start()
		{
			ResetStats(data.Player.CharacterInfo);
			
			foreach (var ally in data.Allies)
			{
				ResetStats(ally.CharacterInfo);
			}
            
			Loader.StartLevel(data);
		}

		private void ResetStats(CharacterInfo info)
		{
			info.StatsInfo.CurrentHealth = info.StatsInfo.MaxHealth;
			info.StatsInfo.CurrentEnergy = info.StatsInfo.MaxEnergy;
			
			foreach (var ability in info.Abilities)
			{
				ability.Avaliable = true;
			}
		}
	}
}
