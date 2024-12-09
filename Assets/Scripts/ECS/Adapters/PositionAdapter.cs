using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class PositionAdapter : EntityBaseComponent<PositionComponent>
	{
		[SerializeField] private Transform _transform;
		[SerializeField] private float _XLimit;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<PositionComponent>();
			component.Value = _transform.position;
		}

		private void Update()
		{
			ref var component = ref Entity.Get<PositionComponent>();
			_transform.position = component.Value;

			if (_XLimit != 0)
			{
				if (_transform.position.x > _XLimit)
				{
					_transform.position = new Vector3(_XLimit, _transform.position.y, _transform.position.z);
				}
				else if (_transform.position.x < -_XLimit)
				{
					_transform.position = new Vector3(-_XLimit, _transform.position.y, _transform.position.z);
				}
			}

			component.Value = _transform.position;
		}

	}
}