using UnityEngine;

namespace Utils
{
	public interface IObstaclePool
	{
		GameObject Get();
		void Return(GameObject obj);
	}
}
