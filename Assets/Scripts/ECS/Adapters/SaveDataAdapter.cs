using DefaultEcs;
using ECS.Components;
using UnityEngine;

namespace ECS.Adapters
{
	public class SaveDataAdapter : EntityBaseComponent<SaveDataComponent>
	{
		[SerializeField] private string _playerName;
		[SerializeField] private int _totalGames;
		[SerializeField] private int _highScore;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<SaveDataComponent>();
			component.PlayerName = _playerName;
			component.TotalGames = _totalGames;
			component.HighScore = _highScore;
		}
	}
}
