using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class TransformAdapter : EntityBaseComponent<TransformComponent>
	{
		[SerializeField] private Transform _transform;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<TransformComponent>();
			component.Transform = _transform;
		}
	}
}