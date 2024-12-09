using MVP.Views;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MVP.Presenters
{
	public class MainMenuPresenter
	{
		private readonly IMainMenuView _view;
		private readonly LeaderboardPresenter _leaderboardPresenter;
		private readonly LoadingManager _loadingManager;

		public MainMenuPresenter(IMainMenuView view, LeaderboardPresenter leaderboardPresenter, LoadingManager loadingManager)
		{
			_view = view;
			_leaderboardPresenter = leaderboardPresenter;
			_loadingManager = loadingManager;

			_view.OnStartGameClicked += HandleStartGame;
			_view.OnLeaderboardClicked += HandleShowLeaderboard;
			_view.OnExitClicked += HandleExitGame;
			_view.EndGameButton += HandeBackToMenu;
			_view.BackToMenu += HandleHideLeaderboardAndGame; 
		}

		public void ShowLoseMenu()
		{
			_view.ShowLoseMenu();
		}

		private void HandeBackToMenu()
		{
			PlayerInputService.OnDipsoseInputService.Invoke();
			_loadingManager.LoadScene("MainMenu");
			HandleHideLeaderboardAndGame();
		}

		private void HandleStartGame()
		{
			_loadingManager.LoadScene("Game");
		}

		private void HandleShowLeaderboard()
		{
			_view.DisableMenu();
			_view.EnableLeadboard();
			_leaderboardPresenter.UpdateLeaderboardUI();
			_leaderboardPresenter.ShowView();
		}

		private void HandleHideLeaderboardAndGame()
		{
			_view.DisableLeaderboard();
			_view.HideLoseMenu();
			_view.EnableMenu();
			_leaderboardPresenter.HideView();
		}

		private void HandleExitGame()
		{
			Application.Quit();
		}
	}
}