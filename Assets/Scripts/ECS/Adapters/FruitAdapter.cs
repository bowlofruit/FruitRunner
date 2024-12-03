﻿using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class FruitAdapter : EntityBaseComponent<FruitComponent>
	{
		[SerializeField] private Color _color;
		[SerializeField] private int _price;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<FruitComponent>();
			component.Color = _color;
			component.Price = _price;
		}
	}
}