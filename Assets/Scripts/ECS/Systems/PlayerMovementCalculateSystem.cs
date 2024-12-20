using DefaultEcs;
using DefaultEcs.System;
using ECS.Components;
using Services;
using UnityEngine;

namespace ECS.Systems
{
	public class PlayerMovementCalculateSystem : AEntitySetSystem<float>
	{
		private readonly PlayerInputService _inputService;

		public PlayerMovementCalculateSystem(World world, PlayerInputService inputService)
			: base(world.GetEntities()
				  .With<PlayerComponent>()
				  .AsSet())
		{
			_inputService = inputService;
		}

		protected override void Update(float deltaTime, in Entity entity)
		{
			if (!entity.IsAlive) return;

			ref var position = ref entity.Get<PositionComponent>();
			ref var speed = ref entity.Get<SpeedComponent>();

			Vector2 input = _inputService.MoveInput;

			Vector3 direction = new Vector3(input.x, 0, 0);
			position.Value += deltaTime * speed.Value * direction;
		}
	}
}