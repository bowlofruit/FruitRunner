using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class GameObjectAdapter : EntityBaseComponent<GameObjectComponent>
	{
		[SerializeField] private GameObject _gameObject;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<GameObjectComponent>();
			component.Value = _gameObject;
		}
	}
}