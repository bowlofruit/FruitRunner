using DefaultEcs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Adapters
{

	public class PlatformAdapter : EntityBaseComponent<PlatformComponent>
	{
		[SerializeField] private int _maxObjects;
		[SerializeField] private Transform _platform;
		[SerializeField] private PlatformType _platformType;

		[SerializeField] private Transform[] _positions;
		[SerializeField] private ObjectTypePosition[] _pos;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<PlatformComponent>();
			var position = _platform.position;
			component.MaxObjects = _maxObjects;

			component.ObjectPositions = new Dictionary<int, Vector3>();
			component.OccupiedPositions = new HashSet<int>();
			component.ObjectTypes = new Dictionary<int, InteractableObjectType>();

			for (int i = 0; i < _positions.Length; i++)
			{
				component.ObjectPositions[i] = _positions[i].position - position;
				component.ObjectTypes[i] = InteractableObjectType.None;
			}

			component.IsLast = true;
			component.PlatformType = _platformType;
		}

	}
}
