using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;
using UnityEngine;
using Utils;

public class PathCleanupSystem : AEntitySetSystem<float>
{
	private readonly IPlatformPool _pathPool;
	private const float CLEANUP_THRESHOLD = 2f;
	private Vector3 _playerPosition;

	public PathCleanupSystem(World world, IPlatformPool pathPool)
		: base(world.GetEntities()
			  .With<PlatformComponent>()
			  .With<GameObjectComponent>()
			  .With<PositionComponent>()
			  .AsSet())
	{
		_pathPool = pathPool;
		_playerPosition = Vector3.zero;
	}

	protected override void PostUpdate(float deltaTime)
	{
		_playerPosition += new Vector3(0, 0, GetPlatformVelocity() * deltaTime);
	}

	protected override void Update(float deltaTime, in Entity entity)
	{
		ref var position = ref entity.Get<PositionComponent>();
		ref var gameObjectComponent = ref entity.Get<GameObjectComponent>();
		ref var platform = ref entity.Get<PlatformComponent>();

		if (position.Value.z < _playerPosition.z - CLEANUP_THRESHOLD)
		{
			Debug.Log($"Platform at Z: {position.Value.z} is out of bounds and will be returned to the pool.");

			foreach (var activeEntity in platform.ActiveObjects)
			{
				if (activeEntity.IsAlive)
				{
					var obj = activeEntity.Get<GameObjectComponent>().Value;
					obj.SetActive(false);
					activeEntity.Dispose();
				}
			}

			_pathPool.Return(gameObjectComponent.Value);
			entity.Dispose();
		}
	}

	private float GetPlatformVelocity()
	{
		foreach (var entity in World.GetEntities()
			.With<PlatformComponent>()
			.With<SpeedComponent>()
			.AsEnumerable())
		{
			var speed = entity.Get<SpeedComponent>();
			return speed.Value;
		}

		return 0f;
	}
}