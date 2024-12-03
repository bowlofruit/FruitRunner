using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class WallBoundsAdapter : EntityBaseComponent<WallBoundsComponent>
	{
		[SerializeField] private float _minX;
		[SerializeField] private float _maxX;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<WallBoundsComponent>();
			component.MinX = _minX;
			component.MaxX = _maxX;
		}
	}
}
