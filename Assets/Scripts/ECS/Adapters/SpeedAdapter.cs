using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class SpeedAdapter : EntityBaseComponent<SpeedComponent>
	{
		[SerializeField] private float _value;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<SpeedComponent>();
			component.Value = _value;
		}
	}
}
