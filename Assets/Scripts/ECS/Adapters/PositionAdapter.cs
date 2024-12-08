using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class PositionAdapter : EntityBaseComponent<PositionComponent>
	{
		[SerializeField] private Transform _transform;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<PositionComponent>();
			component.Value = _transform.position;
		}

		private void Update()
		{
			_transform.position = Entity.Get<PositionComponent>().Value;
		}
	}
}