using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class PlayerAdapter : EntityBaseComponent<PlayerComponent>
	{
		[SerializeField] private int _collectedFruits;
		[SerializeField] private bool _isDead;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<PlayerComponent>();
			component.CollectedFruits = _collectedFruits;
			component.IsDead = _isDead;
		}
	}
}
