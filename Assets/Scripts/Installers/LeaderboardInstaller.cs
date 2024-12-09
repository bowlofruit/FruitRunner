using MVP.Presenters;
using MVP.Views;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class LeaderboardInstaller : MonoInstaller
	{
		[SerializeField] private LeaderboardView _leaderboardView;

		public override void InstallBindings()
		{
			Container.Bind<LeaderboardPresenter>().AsSingle();

			Container.Bind<ILeaderboardView>().To<LeaderboardView>().FromInstance(_leaderboardView).AsSingle();
		}
	}
}