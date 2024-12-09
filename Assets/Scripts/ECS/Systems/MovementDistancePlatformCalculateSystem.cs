using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;
using System.Diagnostics;

public class MovementDistancePlatformCalculateSystem : AEntitySetSystem<float>
{
	public MovementDistancePlatformCalculateSystem(World world)
		: base(world.GetEntities()
			  .With<PlatformComponent>()
			  .With<SpeedComponent>()	
			  .AsSet())
	{

	}

	protected override void Update(float deltaTime, in Entity entity)
	{
		if (!entity.IsAlive) return;

		ref var platformComponent = ref entity.Get<PlatformComponent>();
		ref var speedComponent = ref entity.Get<SpeedComponent>();

		platformComponent.MovementDistance += deltaTime * speedComponent.Value;
    }
}