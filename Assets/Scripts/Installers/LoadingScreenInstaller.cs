using MVP.Presenters;
using MVP.Views;
using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class LoadingScreenInstaller : MonoInstaller
	{
		[SerializeField] private LoadingScreenView _loadingScreenView;
		[SerializeField] private LoadingManager _loadingManager;

		public override void InstallBindings()
		{
			Container.Bind<ILoadingScreenView>().To<LoadingScreenView>().FromInstance(_loadingScreenView).AsSingle();
			Container.Bind<LoadingScreenPresenter>().AsSingle();
			Container.Bind<LoadingManager>().FromInstance(_loadingManager).AsSingle();
		}
	}
}