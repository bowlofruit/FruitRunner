using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace MVP.Views
{
	public class MainMenuView : MonoBehaviour, IMainMenuView
	{
		[Header("MainMenu")]
		[SerializeField] private Canvas _mainMenu;
		[SerializeField] private Button _startButton;
		[SerializeField] private Button _leaderboardButton;
		[SerializeField] private Button _exitButton;

		[Header("Leaderboard")]
		[SerializeField] private Canvas _leaderboard;
		[SerializeField] private Button _backToMenu;

		[Header("Game")]
		[SerializeField] private Canvas _gameMenu;
		[SerializeField] private Button _endGame;

		public event Action OnStartGameClicked;
		public event Action OnLeaderboardClicked;
		public event Action OnExitClicked;
		public event Action EndGameButton;
		public event Action BackToMenu;

		public void Start()
		{
			_startButton.onClick.AddListener(StartButtonClick);
			_leaderboardButton.onClick.AddListener(() => OnLeaderboardClicked?.Invoke());
			_exitButton.onClick.AddListener(() => OnExitClicked?.Invoke());
			_endGame.onClick.AddListener(() => EndGameButton?.Invoke());

			_backToMenu.onClick.AddListener(() => BackToMenu.Invoke());

			_gameMenu.gameObject.SetActive(false);
			_leaderboard.gameObject.SetActive(false);
		}

		private void StartButtonClick()
		{
			OnStartGameClicked?.Invoke();
			_mainMenu.gameObject.SetActive(false);
		}

		public void ShowLoseMenu()
		{
			_gameMenu.gameObject.SetActive(true);
		}

		public void HideLoseMenu()
		{
			_gameMenu.gameObject.SetActive(false);
		}

		public void DisableMenu()
		{
			_startButton.gameObject.SetActive(false);
			_leaderboardButton.gameObject.SetActive(false);
			_exitButton.gameObject.SetActive(false);
		}

		public void EnableMenu()
		{
			_mainMenu.gameObject.SetActive(true);
			_startButton.gameObject.SetActive(true);
			_leaderboardButton.gameObject.SetActive(true);
			_exitButton.gameObject.SetActive(true);
		}

		public void EnableLeadboard()
		{
			_leaderboard.gameObject.SetActive(true);
		}

		public void DisableLeaderboard()
		{
			_leaderboard.gameObject.SetActive(false);
		}
	}
}