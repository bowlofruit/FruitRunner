using UnityEngine;
using System;
using UnityEngine.UI;

namespace MVP.Views
{
	public class MainMenuView : MonoBehaviour, IMainMenuView
	{
		[SerializeField] private Button _startButton;
		[SerializeField] private Button _leaderboardButton;
		[SerializeField] private Button _exitButton;

		public event Action OnStartGameClicked;
		public event Action OnLeaderboardClicked;
		public event Action OnExitClicked;

		private void Awake()
		{
			_startButton.onClick.AddListener(() => OnStartGameClicked?.Invoke());
			_leaderboardButton.onClick.AddListener(() => OnLeaderboardClicked?.Invoke());
			_exitButton.onClick.AddListener(() => OnExitClicked?.Invoke());
		}
	}
}