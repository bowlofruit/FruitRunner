using MVP.Models;
using MVP.Views;
using UnityEngine.SceneManagement;

namespace MVP.Presenters
{
	public class MainMenuPresenter
	{
		private readonly MainMenuModel _model;
		private readonly IMainMenuView _view;

		public MainMenuPresenter(IMainMenuView view)
		{
			_model = new MainMenuModel();
			_view = view;

			_view.OnStartGameClicked += HandleStartGame;
			_view.OnLeaderboardClicked += HandleShowLeaderboard;
			_view.OnExitClicked += HandleExitGame;
		}

		private void HandleStartGame()
		{
			_model.StartGame();
			SceneManager.LoadScene(SceneManager.loadedSceneCount + 1);
		}

		private void HandleShowLeaderboard()
		{
			_model.ShowLeaderboard();

		}

		private void HandleExitGame()
		{
			_model.ExitGame();
		}
	}
}