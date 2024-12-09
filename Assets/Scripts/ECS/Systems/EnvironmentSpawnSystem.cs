using DefaultEcs;
using DefaultEcs.System;
using ECS.Adapters;
using ECS.Components;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnvironmentSpawnSystem : AEntitySetSystem<float>
{
	private readonly World _world;

	private readonly IFruitPool _fruitPool;
	private readonly IObstaclePool _obstaclePool;
	private readonly IEnviromentObjectsPool _enviromentObjectsPool;

	public EnvironmentSpawnSystem(World world, IFruitPool fruitPool, IObstaclePool obstaclePool, IEnviromentObjectsPool enviromentObjectsPool)
		: base(world.GetEntities()
			  .With<PlatformComponent>()
			  .With<PositionComponent>()
			  .AsSet())
	{
		_world = world;
		_fruitPool = fruitPool;
		_obstaclePool = obstaclePool;
		_enviromentObjectsPool = enviromentObjectsPool;
	}

	protected override void Update(float deltaTime, in Entity platformEntity)
	{
		if (!platformEntity.IsAlive) return;

		ref var platform = ref platformEntity.Get<PlatformComponent>();
		var platformPosition = platformEntity.Get<PositionComponent>().Value;

		if (platform.IsObjectInit) return;

		for (int i = 0; i < platform.MaxObjects; i++)
		{
			ProcessObjectSpawn(ref platform, platformPosition, i, platformEntity);
		}

		platform.IsObjectInit = true;
	}

	private void ProcessObjectSpawn(ref PlatformComponent platform, Vector3 platformPosition, int index, Entity platformEntity)
	{
		int randomIndex = FindSuitablePosition(ref platform);

		if (randomIndex == -1)
		{
			Debug.LogWarning("No suitable positions available on platform.");
			return;
		}

		SpawnObject(randomIndex, ref platform, platformPosition, index, platformEntity);
	}

	private int FindSuitablePosition(ref PlatformComponent platform)
	{
		List<int> freePositions = GetFreePositions(ref platform);

		while (freePositions.Count > 0)
		{
			int potentialIndex = freePositions[Random.Range(0, freePositions.Count)];

			if (!HasDeadlyNearby(ref platform, potentialIndex))
			{
				platform.OccupiedPositions.Add(potentialIndex);
				return potentialIndex;
			}

			freePositions.Remove(potentialIndex);
		}

		return -1;
	}

	private List<int> GetFreePositions(ref PlatformComponent platform)
	{
		List<int> freePositions = new();
		foreach (var kvp in platform.ObjectPositions)
		{
			if (!platform.OccupiedPositions.Contains(kvp.Key))
			{
				freePositions.Add(kvp.Key);
			}
		}
		return freePositions;
	}

	private bool HasDeadlyNearby(ref PlatformComponent platform, int potentialIndex)
	{
		if (platform.ObjectTypes.TryGetValue(potentialIndex - 1, out var leftType) && leftType == InteractableObjectType.Deadly)
		{
			return true;
		}
		if (platform.ObjectTypes.TryGetValue(potentialIndex + 1, out var rightType) && rightType == InteractableObjectType.Deadly)
		{
			return true;
		}
		return false;
	}

	private void SpawnObject(int randomIndex, ref PlatformComponent platform, Vector3 platformPosition, int index, Entity platformEntity)
	{
		GameObject obj;
		InteractableObjectType objectType;

		(obj, objectType) = CreateObjectByType(platform.PlatformType, index);

		var entity = obj.GetComponent<EntityBase>().Entity;
		entity.Set(new PlatformObjectComponent()
		{
			Platform = platformEntity
		});

		ref var position = ref entity.Get<PositionComponent>();
		position.Value = platform.ObjectPositions[randomIndex] + platformPosition;
		obj.transform.position = position.Value;

		platform.ObjectTypes[randomIndex] = objectType;
	}

	private (GameObject obj, InteractableObjectType objectType) CreateObjectByType(PlatformType platformType, int index)
	{
		switch (platformType)
		{
			case PlatformType.Interactable:
				bool useObstacles = index % 2 == 0;
				return useObstacles
					? (_obstaclePool.Get(), InteractableObjectType.Deadly)
					: (_fruitPool.Get(), InteractableObjectType.Award);

			case PlatformType.NonInteractable:
				return (_enviromentObjectsPool.Get(), InteractableObjectType.None);

			default:
				Debug.LogError("Non setable platform type");
				return (null, InteractableObjectType.None);
		}
	}
}