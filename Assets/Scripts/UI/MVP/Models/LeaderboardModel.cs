using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MVP.Models
{
	public partial class LeaderboardModel
	{
		private const string PlayerPrefsKey = "Leaderboard";

		public List<ScoreEntry> Scores { get; private set; }

		public LeaderboardModel()
		{
			LoadScores();
		}

		public void AddScore(int score)
		{
			Scores.Add(new ScoreEntry { Score = score });
			Scores = Scores.OrderByDescending(s => s.Score).Take(5).ToList();
			SaveScores();
		}

		private void LoadScores()
		{
			if (PlayerPrefs.HasKey(PlayerPrefsKey))
			{
				string json = PlayerPrefs.GetString(PlayerPrefsKey);
				Scores = JsonUtility.FromJson<ScoreList>(json)?.Entries ?? new List<ScoreEntry>();
			}
			else
			{
				Scores = new List<ScoreEntry>();
			}
		}

		private void SaveScores()
		{
			var scoreList = new ScoreList { Entries = Scores };
			string json = JsonUtility.ToJson(scoreList);
			PlayerPrefs.SetString(PlayerPrefsKey, json);
			PlayerPrefs.Save();
		}
	}
}
