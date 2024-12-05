using DefaultEcs;
using DefaultEcs.System;
using Zenject;

public class SystemInstaller : MonoInstaller
{
	private World _world;

	private ISystem<float> CreateFixedUpdateSystem => new SequentialSystem<float>
		(

		);

	private ISystem<float> CreateUpdateSystem => new SequentialSystem<float>
		(

		);

	private ISystem<float> CreateLateUpdateSystem => new SequentialSystem<float>
		(

		);

	[Inject]
	private void Construct(World world)
	{
		_world = world;
	}

	public override void InstallBindings()
	{
		Container.BindInterfacesAndSelfTo<SystemFixedUpdater>().FromInstance(new SystemFixedUpdater(CreateFixedUpdateSystem)).AsSingle();
		Container.BindInterfacesAndSelfTo<SystemUpdater>().FromInstance(new SystemUpdater(CreateUpdateSystem)).AsSingle();
		Container.BindInterfacesAndSelfTo<SystemLateUpdater>().FromInstance(new SystemLateUpdater(CreateLateUpdateSystem)).AsSingle();
	}
}