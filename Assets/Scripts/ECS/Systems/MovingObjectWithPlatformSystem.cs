using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;

public class MovingObjectWithPlatformSystem : AEntitySetSystem<float>
{
	public MovingObjectWithPlatformSystem(World world)
		: base(world.GetEntities()
			  .With<PlatformComponent>()
			  .AsSet())
	{

	}

	protected override void Update(float deltaTime, in Entity entity)
	{
		ref var platformComponent = ref entity.Get<PlatformComponent>();

		if (!platformComponent.IsObjectInit) return;

        for (int i = 0; i < platformComponent.ObjectPositions.Length && i < platformComponent.MaxObjects; i++)
        {
			platformComponent.ActiveObjects[i].Get<PositionComponent>().Value = platformComponent.ObjectPositions[i];
        }
    }
}