using DefaultEcs;
using DefaultEcs.System;
using ECS.Systems;
using Installers.Updaters;
using MVP.Presenters;
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
	private IEnviromentObjectsPool _enviromentObjectsPool;
	private IEnvironmentPlatformPool _enviromentPlatformPool;

	private ScorePresenter _scorePresenter;
	private LeaderboardPresenter _leaderboardPresenter;

	private ISystem<float> CreateFixedUpdateSystem => new SequentialSystem<float>(
		
	);

	private ISystem<float> CreateUpdateSystem => new SequentialSystem<float>(
		new PlayerMovementCalculateSystem(_world, _playerInputService),
		new PlatformMovementCalculateSystem(_world),
		new MovementDistancePlatformCalculateSystem(_world),
		new ColliderSystem(_world, _fruitPool, _obstaclePool, _scorePresenter, _leaderboardPresenter),
		new InfinitePathGenerationSystem(_world, _pathPool, _enviromentPlatformPool),
		new PathCleanupSystem(_world, _pathPool, _enviromentPlatformPool),
		new PathObjectsCleanupSystem(_world, _fruitPool, _obstaclePool), 
		new EnvironmentSpawnSystem(_world, _fruitPool, _obstaclePool, _enviromentObjectsPool),
		new MovingObjectWithPlatformSystem(_world)
	);

	private ISystem<float> CreateLateUpdateSystem => new SequentialSystem<float>();

	[Inject]
	private void Construct(World world,
						   PlayerInputService playerInputService,
						   IPlatformPool pathPool,
						   IFruitPool fruitPool,
						   IObstaclePool obstaclePool,
						   IEnvironmentPlatformPool environmentPlatformPool,
						   IEnviromentObjectsPool enviromentObjectsPool,
						   ScorePresenter scorePresenter,
						   LeaderboardPresenter leaderboardPresenter)
	{
		_world = world;
		_playerInputService = playerInputService;
		_fruitPool = fruitPool;
		_obstaclePool = obstaclePool;
		_pathPool = pathPool;
		_enviromentObjectsPool = enviromentObjectsPool;
		_enviromentPlatformPool = environmentPlatformPool;
		_scorePresenter = scorePresenter;
		_leaderboardPresenter = leaderboardPresenter;

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