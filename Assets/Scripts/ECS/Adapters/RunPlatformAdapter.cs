using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class RunPlatformAdapter : EntityBaseComponent<RunPlatformComponent>
	{
		[SerializeField] private float _width;
		[SerializeField] private float _length;
		[SerializeField] private float _frequencySpawnTime;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<RunPlatformComponent>();
			component.Width = _width;
			component.Length = _length;
			component.FrequensySpawnTime = _frequencySpawnTime;
		}
	}
}
