using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;

public class MovingObjectWithPlatformSystem : AEntitySetSystem<float>
{
	public MovingObjectWithPlatformSystem(World world)
		: base(world.GetEntities()
			  .With<PlatformObjectComponent>()
			  .With<PositionComponent>()
			  .AsSet())
	{

	}

	protected override void Update(float deltaTime, in Entity entity)
	{
		if (!entity.IsAlive) return;

		var platform = entity.Get<PlatformObjectComponent>().Platform;

		if (!platform.IsAlive) return;

		if (platform.Has<SpeedComponent>())
		{
			var speed = platform.Get<SpeedComponent>().Value;
			entity.Get<PositionComponent>().Value += new UnityEngine.Vector3(0, 0, -(speed * deltaTime));
		}

	}
}