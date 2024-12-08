using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;
using ECS.Systems;
using Installers.Updaters;
using Services;
using Utils;
using Zenject;

public class SystemInstaller : MonoInstaller
{
	private World _world;
	private PlayerInputService _playerInputService;
	private GameObjectPool _pathPool;

	private ISystem<float> CreateFixedUpdateSystem => new SequentialSystem<float>
	(
		new PlayerMovementTransformSystem(_world)
	);

	private ISystem<float> CreateUpdateSystem => new SequentialSystem<float>
	(
		new PlayerMovementCalculateSystem(_world, _playerInputService),
		new PlatformMovementCalculateSystem(_world),
		new InfinitePathGenerationSystem(_world, _pathPool),
		new PathCleanupSystem(_world, _pathPool)
	);

	private ISystem<float> CreateLateUpdateSystem => new SequentialSystem<float>
	(
	);

	[Inject]
	private void Construct(World world, PlayerInputService playerInputService, GameObjectPool gameObjectPool)
	{
		_world = world;
		_playerInputService = playerInputService;
		_pathPool = gameObjectPool;
	}

	public override void InstallBindings()
	{
		Container.BindInterfacesAndSelfTo<SystemFixedUpdater>().FromInstance(new SystemFixedUpdater(CreateFixedUpdateSystem)).AsSingle();
		Container.BindInterfacesAndSelfTo<SystemUpdater>().FromInstance(new SystemUpdater(CreateUpdateSystem)).AsSingle();
		Container.BindInterfacesAndSelfTo<SystemLateUpdater>().FromInstance(new SystemLateUpdater(CreateLateUpdateSystem)).AsSingle();
	}
}
