using Services;
using Zenject;

namespace Installers
{
	public class PlayerInputInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<PlayerInputService>().AsSingle().NonLazy();
		}
	}
}