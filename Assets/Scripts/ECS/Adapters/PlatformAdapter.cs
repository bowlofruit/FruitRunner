using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class PlatformAdapter : EntityBaseComponent<PlatformComponent>
	{
		[SerializeField] private int _maxObjects;
		[SerializeField] private Transform[] _positions;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<PlatformComponent>();
			component.MaxObjects = _maxObjects;
			
			component.ObjectPositions = new Vector3[_positions.Length];
            for (int i = 0; i < _positions.Length; i++)
            {
				component.ObjectPositions[i] = _positions[i].position;
            }

			component.ActiveObjects = new Entity[_positions.Length];

			component.IsLast = true;
        }

		private void Update()
		{
			ref var component = ref Entity.Get<PlatformComponent>();
			for (int i = 0; i < _positions.Length; i++)
			{
				component.ObjectPositions[i] = _positions[i].position;
			}
		}
	}
}
