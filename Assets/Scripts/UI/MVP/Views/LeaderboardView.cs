using TMPro;
using UnityEngine;

namespace MVP.Views
{
	public class LeaderboardView : MonoBehaviour, ILeaderboardView
	{
		[SerializeField] private TMP_Text _leaderboardText;

		public void HideScores()
		{
			_leaderboardText.gameObject.SetActive(false);
		}

		public void ShowScores()
		{
			_leaderboardText.gameObject.SetActive(true);
		}

		public void SyncScores(string scores)
		{
			_leaderboardText.text = scores;
		}
	}
}