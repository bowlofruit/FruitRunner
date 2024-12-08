using UnityEngine;

namespace Utils
{
	public interface IFruitPool
	{
		GameObject Get();
		void Return(GameObject obj);
	}
}
