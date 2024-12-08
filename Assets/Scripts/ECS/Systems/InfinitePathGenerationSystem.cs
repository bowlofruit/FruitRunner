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