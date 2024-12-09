using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;
using Utils;

public class PathObjectsCleanupSystem : AEntitySetSystem<float>
{
	private readonly IFruitPool _fruitPool;
	private readonly IObstaclePool _obstaclesPool;
	private readonly IEnviromentObjectsPool _enviromentObjectsPool;

	public PathObjectsCleanupSystem(World world, IFruitPool fruitPool, IObstaclePool obstaclePool, IEnviromentObjectsPool enviromentObjectsPool)
		: base(world.GetEntities()
			  .With<GameObjectComponent>()
			  .With<PlatformObjectComponent>()
			  .AsSet())
	{
		_fruitPool = fruitPool;
		_obstaclesPool = obstaclePool;
		_enviromentObjectsPool = enviromentObjectsPool;
	}

	protected override void Update(float deltaTime, in Entity entity)
	{
		if (!entity.IsAlive) return;

		if (entity.Get<PlatformObjectComponent>().Platform.IsAlive) return;

		ref var gameObject = ref entity.Get<GameObjectComponent>();

		if (entity.Has<FruitComponent>())
		{
			_fruitPool.Return(gameObject.Value);
		}
		else if (entity.Has<ObstacleComponent>())
		{
			_obstaclesPool.Return(gameObject.Value);
		}
		else
		{
			_enviromentObjectsPool.Return(gameObject.Value);
		}
	}
}