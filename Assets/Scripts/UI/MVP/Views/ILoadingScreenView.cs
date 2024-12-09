namespace MVP.Views
{
	public interface ILoadingScreenView
	{
		void Show();
		void Hide();
		void UpdateProgress(float progress);
	}
}