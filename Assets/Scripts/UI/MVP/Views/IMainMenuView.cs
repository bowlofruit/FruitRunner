using System;

namespace MVP.Views
{
	public interface IMainMenuView
	{
		event Action OnStartGameClicked;
		event Action OnLeaderboardClicked;
		event Action OnExitClicked;
		event Action EndGameButton;
		event Action BackToMenu;

		public void ShowLoseMenu();
		public void HideLoseMenu();
		public void DisableMenu();
		public void EnableMenu();
		public void DisableLeaderboard();
		public void EnableLeadboard();

	}
}