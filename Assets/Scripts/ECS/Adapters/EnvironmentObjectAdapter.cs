using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class EnvironmentObjectAdapter : EntityBaseComponent<EnvironmentObjectComponent>
	{
		[SerializeField] private int _type;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<EnvironmentObjectComponent>();
			component.Type = _type;
		}
	}
}
