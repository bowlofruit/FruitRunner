using MVP.Models;
using MVP.Presenters;
using MVP.Views;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class ScoreInstaller : MonoInstaller
	{
		[SerializeField] private ScoreView _scoreView;

		public override void InstallBindings()
		{
			Container.Bind<ScorePresenter>().AsSingle();

			Container.Bind<IScoreView>().To<ScoreView>().FromInstance(_scoreView).AsSingle();
		}
	}
}