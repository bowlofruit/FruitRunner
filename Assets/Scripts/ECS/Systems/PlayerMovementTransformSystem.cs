using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;

namespace ECS.Systems
{
	public class PlayerMovementTransformSystem : AEntitySetSystem<float>
	{
		public PlayerMovementTransformSystem(World world)
			: base(world.GetEntities()
				  .With<PositionComponent>()
				  .With<TransformComponent>()
				  .AsSet())
		{
		}

		protected override void Update(float deltaTime, in Entity entity)
		{
			ref var position = ref entity.Get<PositionComponent>();
			ref var transform = ref entity.Get<TransformComponent>();

			transform.Transform.position = position.Value;
		}
	}
}