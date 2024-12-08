using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class ScaleAdapter : EntityBaseComponent<ScaleComponent>
	{
		[SerializeField] private Vector3 _scale;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<ScaleComponent>();
			component.X = _scale.x;
			component.Y = _scale.y;
			component.Z = _scale.z;
		}
	}
}