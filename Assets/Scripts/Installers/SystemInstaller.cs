using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;
using ECS.Systems;
using Installers.Updaters;
using Services;
using UnityEngine;
using Utils;
using Zenject;

public class SystemInstaller : MonoInstaller
{
	private World _world;
	private PlayerInputService _playerInputService;

	private IPlatformPool _pathPool;
	private IFruitPool _fruitPool;
	private IObstaclePool _obstaclePool;

	private ISystem<float> CreateFixedUpdateSystem => new SequentialSystem<float>(
		
	);

	private ISystem<float> CreateUpdateSystem => new SequentialSystem<float>(
		new PlayerMovementCalculateSystem(_world, _playerInputService),
		new PlatformMovementCalculateSystem(_world),
		new ColliderSystem(_world),
		new InfinitePathGenerationSystem(_world, _pathPool),
		new PathCleanupSystem(_world, _pathPool),
		new EnvironmentSpawnSystem(_world, _fruitPool, _obstaclePool),
		new MovingObjectWithPlatformSystem(_world)
	);

	private ISystem<float> CreateLateUpdateSystem => new SequentialSystem<float>();

	[Inject]
	private void Construct(World world,
						   PlayerInputService playerInputService,
						   IPlatformPool pathPool,
						   IFruitPool fruitPool,
						   IObstaclePool obstaclePool)
	{
		_world = world;
		_playerInputService = playerInputService;
		_fruitPool = fruitPool;
		_obstaclePool = obstaclePool;
		_pathPool = pathPool;

		Debug.Log("SystemInstaller constructed with dependencies.");
	}

	public override void InstallBindings()
	{
		Container.BindInterfacesAndSelfTo<SystemFixedUpdater>()
				 .FromInstance(new SystemFixedUpdater(CreateFixedUpdateSystem))
				 .AsSingle();

		Container.BindInterfacesAndSelfTo<SystemUpdater>()
				 .FromInstance(new SystemUpdater(CreateUpdateSystem))
				 .AsSingle();

		Container.BindInterfacesAndSelfTo<SystemLateUpdater>()
				 .FromInstance(new SystemLateUpdater(CreateLateUpdateSystem))
				 .AsSingle();
	}
}