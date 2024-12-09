using DefaultEcs;
using Zenject;

namespace Installers
{
	public class ECSInstaller : MonoInstaller
	{
		private World _world;
		public override void InstallBindings()
		{
			_world = new World();
			Container.Bind<World>().FromInstance(_world).AsSingle().NonLazy();
		}
	}
}