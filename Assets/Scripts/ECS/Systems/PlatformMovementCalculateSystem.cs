using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;

namespace ECS.Systems
{
	public class PlatformMovementCalculateSystem : AEntitySetSystem<float>
	{
		public PlatformMovementCalculateSystem(World world)
			: base(world.GetEntities()
					.With<PlatformComponent>()
					.With<PositionComponent>()
					.With<SpeedComponent>()
					.AsSet())
			{
		}

		protected override void Update(float deltaTime, in Entity entity)
		{
			ref var position = ref entity.Get<PositionComponent>();
			ref var speed = ref entity.Get<SpeedComponent>();

			position.Value.z -= speed.Value * deltaTime;
		}
	}
}