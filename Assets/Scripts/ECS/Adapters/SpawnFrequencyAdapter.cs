using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class SpawnFrequencyAdapter : EntityBaseComponent<SpawnFrequencyComponent>
	{
		[SerializeField] private float _frequencySpawnTime;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<SpawnFrequencyComponent>();
			component.FrequensySpawnTime = _frequencySpawnTime;
		}
	}
}
