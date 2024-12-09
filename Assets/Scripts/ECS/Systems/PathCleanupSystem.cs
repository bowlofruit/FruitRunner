using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;
using UnityEngine;
using Utils;

public class PathCleanupSystem : AEntitySetSystem<float>
{
	private readonly IPlatformPool _pathPool;
	private readonly IEnvironmentPlatformPool _environmentPlatformPool;
	private const float CLEANUP_THRESHOLD = 1f;

	public PathCleanupSystem(World world, IPlatformPool pathPool, IEnvironmentPlatformPool environmentPlatformPool)
		: base(world.GetEntities()
			  .With<PlatformComponent>()
			  .With<GameObjectComponent>()
			  .With<PositionComponent>()
			  .With<ScaleComponent>()
			  .AsSet())
	{
		_pathPool = pathPool;
		_environmentPlatformPool = environmentPlatformPool;
	}

	protected override void Update(float deltaTime, in Entity entity)
	{
		if (!entity.IsAlive)
		{
			Debug.LogWarning("Entity is not alive, skipping cleanup.");
			return;
		}

		ref var platform = ref entity.Get<PlatformComponent>();
		ref var gameObject = ref entity.Get<GameObjectComponent>();

		var length = entity.Get<ScaleComponent>().Z;

		Debug.Log($"Checking platform for cleanup: PlatformType = {platform.PlatformType}, MovementDistance = {platform.MovementDistance}, Length = {length}, Threshold = {length * CLEANUP_THRESHOLD}");

		if (platform.MovementDistance > length * CLEANUP_THRESHOLD && !platform.IsLast)
		{
			Debug.Log($"Cleaning platform of type {platform.PlatformType}. Position: {entity.Get<PositionComponent>().Value}");

			switch (platform.PlatformType)
			{
				case PlatformType.Interactable:
					Debug.Log("Returning platform to the interactable pool.");
					_pathPool.Return(gameObject.Value);
					break;

				case PlatformType.NonInteractable:
					Debug.Log("Returning platform to the non-interactable pool.");
					_environmentPlatformPool.Return(gameObject.Value);
					break;

				default:
					Debug.LogError("Unknown PlatformType encountered during cleanup.");
					break;
			}

			Debug.Log($"Clearing occupied positions. Count: {platform.OccupiedPositions.Count}");
			platform.OccupiedPositions.Clear();

			Debug.LogWarning($"Platform cleanup completed for PlatformType: {platform.PlatformType}");
		}
		else
		{
			Debug.Log("Platform does not meet cleanup criteria.");
		}
	}

}