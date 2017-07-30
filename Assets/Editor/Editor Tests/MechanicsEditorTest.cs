using UnityEditor;
using UnityEngine;
using CharacterController = Game.Character.CharacterInfoController;

public class MechanicsEditorTest : EditorWindow {

	[MenuItem("Window/Editor Tests/Mechanics Test")]
	public static void ShowWindow()
	{
		GetWindow<MechanicsEditorTest>("Mechanics Test");
	}

	private void OnGUI()
	{
		if (GUILayout.Button("Deal 15 damage to <playerName>"))
		{
			if (!Application.isPlaying)
			{
				Debug.LogWarning("Start game to execute this test.");
				return;
			}

			var player = GameObject.Find("YOU (test)").GetComponent<CharacterController>();
			if (player != null)
			{
				player.DealDamage(15);
			}
			else
			{
				Debug.LogWarning("No gameobject with this name.");
			}
		}
		
		if (GUILayout.Button("Heal 15 health to <playerName>"))
		{
			if (!Application.isPlaying)
			{
				Debug.LogWarning("Start game to execute this test.");
				return;
			}

			var player = GameObject.Find("YOU (test)").GetComponent<CharacterController>();
			if (player != null)
			{
				player.Heal(15);
			}
			else
			{
				Debug.LogWarning("No gameobject with this name.");
			}
		}
	}
}
