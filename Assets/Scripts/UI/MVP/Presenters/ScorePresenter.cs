using MVP.Models;
using MVP.Views;
using System.Linq;
using Zenject;

namespace MVP.Presenters
{
	public class ScorePresenter : IInitializable
	{
		private readonly ScoreModel _scoreModel;
		private readonly IScoreView _scoreView;

		public ScorePresenter(IScoreView scoreView)
		{
			_scoreModel = new ScoreModel();
			_scoreView = scoreView;
		}

		public void Initialize()
		{
			UpdateScoreUI();
		}

		private void UpdateScoreUI()
		{
			_scoreView.SetScoreText(_scoreModel.Score.ToString());
		}

		public void OnScoreChanged()
		{
			UpdateScoreUI();
		}

		public void AddScore(int amount)
		{
			_scoreModel.AddScore(amount);
			OnScoreChanged();
		}
	}
}