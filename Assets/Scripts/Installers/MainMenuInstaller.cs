using MVP.Presenters;
using MVP.Views;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class MainMenuInstaller : MonoInstaller
	{
		[SerializeField] private MainMenuView _mainMenuView;

		public override void InstallBindings()
		{
			Container.Bind<IMainMenuView>().To<MainMenuView>().FromInstance(_mainMenuView).AsSingle();

			Container.Bind<MainMenuPresenter>().AsSingle().NonLazy();
		}
	}
}