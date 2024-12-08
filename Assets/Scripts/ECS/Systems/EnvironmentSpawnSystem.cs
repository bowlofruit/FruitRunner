using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;
using UnityEngine;
using Utils;

public class EnvironmentSpawnSystem : AEntitySetSystem<float>
{
	private readonly World _world;
	private readonly IFruitPool _fruitPool;
	private readonly IObstaclePool _obstaclePool;

	public EnvironmentSpawnSystem(World world, IFruitPool fruitPool, IObstaclePool obstaclePool)
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

		if (platform.IsObjectInit) return;

		if (platform.ActiveObjects == null || platform.ActiveObjects.Length != platform.ObjectPositions.Length)
		{
			platform.ActiveObjects = new Entity[platform.ObjectPositions.Length];
		}

		for (int i = 0; i < platform.ObjectPositions.Length && i < platform.MaxObjects; i++)
		{
			if (!platform.ActiveObjects[i].IsAlive)
			{
				var useFruit = i % 2 == 0;

				GameObject obj;
				if (useFruit)
				{
					obj = _fruitPool.Get();
				}
				else
				{
					obj = _obstaclePool.Get();
				}

				platform.ActiveObjects[i] = obj.GetComponent<EntityBase>().Entity;
			}
			else
			{
				ref var position = ref platform.ActiveObjects[i].Get<PositionComponent>();
				position.Value = platform.ObjectPositions[i];

				var obj = platform.ActiveObjects[i].Get<GameObjectComponent>().Value;
				obj.transform.position = platform.ObjectPositions[i];
				obj.SetActive(true);
			}

			platform.IsObjectInit = true;
		}
	}
}