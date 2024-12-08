using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;
using UnityEngine;
using Utils;

public class InfinitePathGenerationSystem : AEntitySetSystem<float>
{
	private readonly World _world;
	private readonly GameObjectPool _pathPool;
	private float _segmentLength;
	private bool _isPlatformSpawned;
	private float _movedDistance;
	private const float SPAWN_OFFSET = 0.5f;
	private Vector3 _lastSegmentPosition;

	public InfinitePathGenerationSystem(World world, GameObjectPool pathPool)
		: base(world.GetEntities()
			  .With<PlatformComponent>()
			  .AsSet())
	{
		_world = world;
		_pathPool = pathPool;

		_isPlatformSpawned = false;
		_lastSegmentPosition = Vector3.zero;
		GeneratePathSegment();
	}

	protected override void PostUpdate(float deltaTime)
	{
		if (_segmentLength == 0f)
		{
			_segmentLength = GetPlatformSegmentLength() - SPAWN_OFFSET;
		}

		float platformSpeed = GetPlatformVelocity();
		_movedDistance += platformSpeed * deltaTime;

		if (_movedDistance > _segmentLength && !_isPlatformSpawned)
		{
			GeneratePathSegment();
			_movedDistance = 0;
			_isPlatformSpawned = false;
		}
	}

	private void GeneratePathSegment()
	{
		GameObject segmentObject = _pathPool.Get();
		segmentObject.transform.position = _lastSegmentPosition;

		Entity entity = _world.CreateEntity();
		entity.Set(new PlatformComponent());
		entity.Set(new GameObjectComponent { Value = segmentObject });
		entity.Set(new PositionComponent { Value = _lastSegmentPosition });
		entity.Set(new ScaleComponent());

		_lastSegmentPosition += new Vector3(0, 0, -_segmentLength);
	}

	private float GetPlatformSegmentLength()
	{
		foreach (var entity in _world.GetEntities()
			.With<PlatformComponent>()
			.With<ScaleComponent>()
			.AsEnumerable())
		{
			var scale = entity.Get<ScaleComponent>();
			return scale.Z;
		}

		Debug.Log("Scale component doesn't exist");
		return 0f;
	}

	private float GetPlatformVelocity()
	{
		foreach (var entity in _world.GetEntities()
			.With<PlatformComponent>()
			.With<SpeedComponent>()
			.AsEnumerable())
		{
			var speed = entity.Get<SpeedComponent>();
			return speed.Value;
		}

		Debug.Log("Speed component not found, defaulting to zero.");
		return 0f;
	}
}

public class EnvironmentSpawnSystem : AEntitySetSystem<float>
{
	private readonly World _world;
	private readonly GameObjectPool _fruitPool;
	private readonly GameObjectPool _obstaclePool;

	public EnvironmentSpawnSystem(World world, GameObjectPool fruitPool, GameObjectPool obstaclePool)
		: base(world.GetEntities()
			  .With<PlatformComponent>()
			  .AsSet())
	{
		_world = world;
		_fruitPool = fruitPool;
		_obstaclePool = obstaclePool;
	}

	protected override void Update(float deltaTime, in Entity platformEntity)
	{
		ref var platform = ref platformEntity.Get<PlatformComponent>();

		if (platform.ActiveObjects == null || platform.ActiveObjects.Length != platform.ObjectPositions.Length)
		{
			platform.ActiveObjects = new Entity[platform.ObjectPositions.Length];
		}

		for (int i = 0; i < platform.ObjectPositions.Length && i < platform.MaxObjects; i++)
		{
			if (!platform.ActiveObjects[i].IsAlive)
			{
				var useFruit = i % 2 == 0;

				GameObject obj = useFruit ? _fruitPool.Get() : _obstaclePool.Get();
				obj.transform.position = platform.ObjectPositions[i];

				var entity = _world.CreateEntity();
				if (useFruit)
				{
					entity.Set(new FruitComponent { Price = Random.Range(1, 10) });
				}
				else
				{
					entity.Set(new ObstacleComponent { IsDeadly = true });
				}

				entity.Set(new GameObjectComponent { Value = obj });
				entity.Set(new PositionComponent { Value = platform.ObjectPositions[i] });

				platform.ActiveObjects[i] = entity;
			}
			else
			{
				ref var position = ref platform.ActiveObjects[i].Get<PositionComponent>();
				position.Value = platform.ObjectPositions[i];

				var obj = platform.ActiveObjects[i].Get<GameObjectComponent>().Value;
				obj.transform.position = platform.ObjectPositions[i];
				obj.SetActive(true);
			}
		}
	}
}