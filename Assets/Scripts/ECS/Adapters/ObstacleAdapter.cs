using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class ObstacleAdapter : EntityBaseComponent<ObstacleComponent>
	{
		[SerializeField] private bool _isDeadly;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<ObstacleComponent>();
			component.IsDeadly = _isDeadly;
		}
	}
}
