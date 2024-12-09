using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Utils;

public class InfinitePathGenerationSystem : AEntitySetSystem<float>
{
	private readonly World _world;
	private readonly IPlatformPool _pathPool;
	private readonly IEnvironmentPlatformPool _environmentPlatformPool;

	private const float SPAWN_OFFSET = 0.75f;
	private Dictionary<PlatformType, int> _spawnForwardCount = new();

	private const int SPAWN_FORWARD = 3;

	public InfinitePathGenerationSystem(World world, IPlatformPool pathPool, IEnvironmentPlatformPool environmentPlatformPool)
		: base(world.GetEntities()
			  .With<PlatformComponent>()
			  .With<ScaleComponent>()
			  .AsSet())
	{
		_world = world;
		_pathPool = pathPool;
		_environmentPlatformPool = environmentPlatformPool;
	}

	protected override void Update(float deltaTime, in Entity entity)
	{
		if (!entity.IsAlive) return;

		var platform = entity.Get<PlatformComponent>();
		if (platform.IsLast == false) return;

		var lenght = entity.Get<ScaleComponent>().Z;
		_spawnForwardCount.TryAdd(platform.PlatformType, 0);

		if(_spawnForwardCount[platform.PlatformType] < SPAWN_FORWARD)
		{
			_spawnForwardCount[platform.PlatformType]++;
			GeneratePathSegment(entity, -_spawnForwardCount[platform.PlatformType], platform.PlatformType);
		}
		else if (platform.MovementDistance > lenght)
		{
			GeneratePathSegment(entity, -_spawnForwardCount[platform.PlatformType], platform.PlatformType);
		}
	}

	private void GeneratePathSegment(Entity platform, int platformDistanceOffset, PlatformType platformType)
	{
		var platformLenght = platform.Get<ScaleComponent>().Z;
		var nextPosition = platform.Get<PositionComponent>().Value + new Vector3(0,0, platformLenght - SPAWN_OFFSET);

		GameObject segmentObject = platformType switch
		{
			PlatformType.Interactable => _pathPool.Get(),
			PlatformType.NonInteractable => _environmentPlatformPool.Get(),
			_ => throw new System.Exception()
		};
		var entity = segmentObject.GetComponent<EntityBase>().Entity;
		segmentObject.transform.position = nextPosition;

		entity.Get<PositionComponent>().Value = nextPosition;

		ref var platformComponent = ref platform.Get<PlatformComponent>();
		platformComponent.IsLast = false;
		
		platformComponent.MovementDistance = platformDistanceOffset * platformLenght;
	}
}
