using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class FruitAdapter : EntityBaseComponent<FruitComponent>
	{
		[SerializeField] private int _price;
		[SerializeField] private FruitType _fruitType;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<FruitComponent>();
			component.Price = _price;
			component.Type = _fruitType;
		}
	}
}