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

		public MainMenuPresenter(IMainMenuView view, LeaderboardPresenter leaderboardPresenter)
		{
			_view = view;
			_leaderboardPresenter = leaderboardPresenter;

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
			SceneManager.LoadScene(0);
			HandleHideLeaderboardAndGame();
		}

		private void HandleStartGame()
		{
			SceneManager.LoadScene(1);
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