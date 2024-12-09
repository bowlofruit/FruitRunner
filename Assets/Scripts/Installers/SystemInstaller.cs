using DefaultEcs;
using DefaultEcs.System;
using ECS.Systems;
using Installers.Updaters;
using MVP.Presenters;
using Services;
using System;
using UnityEngine;
using Utils;
using Zenject;

public class SystemInstaller : MonoInstaller
{
	public static Action<bool> TimeFreezer;

	private ISystem<float> _fixedUpdateSystem;
	private ISystem<float> _updateSystem;
	private ISystem<float> _lateUpdateSystem;

	private World _world;
	private PlayerInputService _playerInputService;

	private IPlatformPool _pathPool;
	private IFruitPool _fruitPool;
	private IObstaclePool _obstaclePool;
	private IEnviromentObjectsPool _enviromentObjectsPool;
	private IEnvironmentPlatformPool _enviromentPlatformPool;

	private ScorePresenter _scorePresenter;
	private LeaderboardPresenter _leaderboardPresenter;
	private MainMenuPresenter _mainMenuPresenter;

	private ISystem<float> CreateFixedUpdateSystem => new SequentialSystem<float>(
		
	);

	private ISystem<float> CreateUpdateSystem => new SequentialSystem<float>(
		new PlayerMovementCalculateSystem(_world, _playerInputService),
		new PlatformMovementCalculateSystem(_world),
		new MovementDistancePlatformCalculateSystem(_world),
		new ColliderSystem(_world, _fruitPool, _obstaclePool, _scorePresenter, _leaderboardPresenter, _mainMenuPresenter),
		new InfinitePathGenerationSystem(_world, _pathPool, _enviromentPlatformPool),
		new PathCleanupSystem(_world, _pathPool, _enviromentPlatformPool),
		new PathObjectsCleanupSystem(_world, _fruitPool, _obstaclePool, _enviromentObjectsPool), 
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
						   LeaderboardPresenter leaderboardPresenter,
						   MainMenuPresenter mainMenuPresenter)
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
		_mainMenuPresenter = mainMenuPresenter;

		Debug.Log("SystemInstaller constructed with dependencies.");
	}

	public override void InstallBindings()
	{
		_fixedUpdateSystem = CreateFixedUpdateSystem;
		_updateSystem = CreateUpdateSystem;
		_lateUpdateSystem = CreateLateUpdateSystem;

		Container.BindInterfacesAndSelfTo<SystemFixedUpdater>()
				 .FromInstance(new SystemFixedUpdater(_fixedUpdateSystem))
				 .AsSingle();

		Container.BindInterfacesAndSelfTo<SystemUpdater>()
				 .FromInstance(new SystemUpdater(_updateSystem))
				 .AsSingle();

		Container.BindInterfacesAndSelfTo<SystemLateUpdater>()
				 .FromInstance(new SystemLateUpdater(_lateUpdateSystem))
				 .AsSingle();

		TimeFreezer = IsTimeFreeze;
	}

	private void OnDestroy()
	{
		TimeFreezer = null;
	}

	private void IsTimeFreeze(bool isFreeze)
	{
		_fixedUpdateSystem.IsEnabled = !isFreeze;
		_updateSystem.IsEnabled = !isFreeze;
		_lateUpdateSystem.IsEnabled = !isFreeze;
	}
}