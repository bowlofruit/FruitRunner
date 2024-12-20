﻿using MVP.Models;
using MVP.Views;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MVP.Presenters
{
	public class LeaderboardPresenter
	{
		private readonly LeaderboardModel _leaderboardModel;
		private readonly ILeaderboardView _leaderboardView;

		public LeaderboardPresenter(ILeaderboardView leaderboardView)
		{
			_leaderboardModel = new LeaderboardModel();
			_leaderboardView = leaderboardView;
		}

		public void ShowView()
		{
			_leaderboardView.ShowScores();
		}

		public void HideView()
		{
			_leaderboardView.HideScores();
		}

		public void AddScore(int score)
		{
			_leaderboardModel.AddScore(score);
			UpdateLeaderboardUI();
		}

		public void UpdateLeaderboardUI()
		{
			var displayText = string.Join("\n", _leaderboardModel.Scores
				.Select((entry, index) => $"{index + 1}. {entry.Score}"));
			_leaderboardView.SyncScores(displayText);
		}
	}
}