using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class ScaleAdapter : EntityBaseComponent<ScaleComponent>
	{
		[SerializeField] private float _scaleX;
		[SerializeField] private float _scaleY;
		[SerializeField] private float _scaleZ;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<ScaleComponent>();
			component.ScaleX = _scaleX;
			component.ScaleY = _scaleY;
			component.ScaleZ = _scaleZ;
		}
	}
}