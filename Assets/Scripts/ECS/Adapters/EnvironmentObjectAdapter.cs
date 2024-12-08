using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	enum GeneratedObjectType
	{
		Interactable,
		NonInteractable
	}

	public class EnvironmentObjectAdapter : EntityBaseComponent<EnvironmentObjectComponent>
	{
		[SerializeField] private GeneratedObjectType _type;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<EnvironmentObjectComponent>();
			component.Type = (int)_type;
		}
	}
}
