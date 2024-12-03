﻿using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class EnvironmentAdapter : EntityBaseComponent<EnvironmentComponent>
	{
		[SerializeField] private int _maxObjects;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<EnvironmentComponent>();
			component.MaxObjects = _maxObjects;
		}
	}
}