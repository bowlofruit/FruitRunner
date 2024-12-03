using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class PositionAdapter : EntityBaseComponent<PositionComponent>
	{
		[SerializeField] private Vector3 _position;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<PositionComponent>();
			component.Position = _position;
		}
	}
}
