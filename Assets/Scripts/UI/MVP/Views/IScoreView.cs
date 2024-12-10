using ECS.Components;
using System.Collections.Generic;

namespace MVP.Views
{
	public interface IScoreView
	{
		void SetScoreText(string score);
		void SetFruitCounts(Dictionary<FruitType, int> fruitCounts);
	}
}