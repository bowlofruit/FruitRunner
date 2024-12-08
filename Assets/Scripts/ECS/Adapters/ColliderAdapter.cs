using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class ColliderAdapter : EntityBaseComponent<ColliderComponent>
	{
		[SerializeField] private float _radius;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<ColliderComponent>();
			component.Radius = _radius;
		}
	}
}