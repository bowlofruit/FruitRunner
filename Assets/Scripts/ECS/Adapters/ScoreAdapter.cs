using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class ScoreAdapter : EntityBaseComponent<ScoreComponent>
	{
		[SerializeField] private int _totalPoints;
		[SerializeField] private int _redFruits;
		[SerializeField] private int _greenFruits;
		[SerializeField] private int _blueFruits;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<ScoreComponent>();
			component.TotalPoints = _totalPoints;
			component.RedFruits = _redFruits;
			component.GreenFruits = _greenFruits;
			component.BlueFruits = _blueFruits;
		}
	}
}
