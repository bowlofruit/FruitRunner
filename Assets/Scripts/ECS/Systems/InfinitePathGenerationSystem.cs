using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;
using UnityEngine;
using Utils;

public class InfinitePathGenerationSystem : AEntitySetSystem<float>
{
	private readonly World _world;
	private readonly IPlatformPool _pathPool;
	private float _movedDistance;

	private const float SPAWN_OFFSET = 0.5f;

	public InfinitePathGenerationSystem(World world, IPlatformPool pathPool)
		: base(world.GetEntities()
			  .With<PlatformComponent>()
			  .With<ScaleComponent>()
			  .With<SpeedComponent>()
			  .AsSet())
	{
		_world = world;
		_pathPool = pathPool;
	}

	protected override void Update(float deltaTime, in Entity entity)
	{
		if (entity.Get<PlatformComponent>().IsLast == false) return;

		var lenght = entity.Get<ScaleComponent>().Z - SPAWN_OFFSET;

		float platformSpeed = entity.Get<SpeedComponent>().Value;
		_movedDistance += platformSpeed * deltaTime;

		if (_movedDistance > lenght)
		{
			GeneratePathSegment(entity);
			_movedDistance = 0;
		}
	}

	private void GeneratePathSegment(Entity platform)
	{
		var nextPosition = platform.Get<PositionComponent>().Value + new Vector3(0,0, platform.Get<ScaleComponent>().Z);
		GameObject segmentObject = _pathPool.Get();
		var entity = segmentObject.GetComponent<EntityBase>().Entity;
		segmentObject.transform.position = nextPosition;

		entity.Get<PositionComponent>().Value = nextPosition;

		platform.Get<PlatformComponent>().IsLast = false;
	}
}
