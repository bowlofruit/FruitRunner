using ECS.Components;
using System.Collections.Generic;

namespace MVP.Models
{
	public class ScoreModel
	{
		public int Score { get; private set; }
		public Dictionary<FruitType, int> FruitCounts { get; private set; }

		public ScoreModel()
		{
			FruitCounts = new Dictionary<FruitType, int>();
			foreach (FruitType type in System.Enum.GetValues(typeof(FruitType)))
			{
				FruitCounts[type] = 0;
			}
		}

		public void AddScore(int amount)
		{
			Score += amount;
		}

		public void IncrementFruitCount(FruitType type)
		{
			FruitCounts[type]++;
		}
	}
}
