using InputSystem;
using Zenject;

namespace Installers
{
	public class GameInputInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<PlayerInput_Action>().AsSingle().NonLazy();
		}
	}
}

