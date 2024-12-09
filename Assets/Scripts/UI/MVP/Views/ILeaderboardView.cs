namespace MVP.Views
{
	public interface ILeaderboardView
	{
		public void ShowScores();
		public void HideScores();
		public void SyncScores(string scores);
	}
}