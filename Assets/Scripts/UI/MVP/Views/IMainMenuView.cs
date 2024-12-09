using System;

namespace MVP.Views
{
	public interface IMainMenuView
	{
		event Action OnStartGameClicked;
		event Action OnLeaderboardClicked;
		event Action OnExitClicked;
	}
}