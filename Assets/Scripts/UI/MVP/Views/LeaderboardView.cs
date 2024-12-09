using TMPro;
using UnityEngine;

namespace MVP.Views
{
	public class LeaderboardView : MonoBehaviour, ILeaderboardView
	{
		[SerializeField] private TMP_Text leaderboardText;

		public void DisplayScores(string scores)
		{
			leaderboardText.text = scores;
		}
	}
}