using ECS.Components;
using MVP.Models;
using MVP.Views;
using Zenject;

namespace MVP.Presenters
{
	public class ScorePresenter
	{
		private readonly ScoreModel _scoreModel;
		private readonly IScoreView _scoreView;

		public ScorePresenter(IScoreView scoreView)
		{
			_scoreModel = new ScoreModel();
			_scoreView = scoreView;

			UpdateScoreUI();
		}

		private void UpdateScoreUI()
		{
			_scoreView.SetScoreText(_scoreModel.Score.ToString());
			_scoreView.SetFruitCounts(_scoreModel.FruitCounts);
		}

		public void OnScoreChanged()
		{
			UpdateScoreUI();
		}

		public void AddScore(int amount, FruitType type)
		{
			_scoreModel.AddScore(amount);
			_scoreModel.IncrementFruitCount(type);
			OnScoreChanged();
		}
	}
}
