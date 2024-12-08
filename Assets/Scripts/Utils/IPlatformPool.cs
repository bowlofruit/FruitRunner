using UnityEngine;

namespace Utils
{
	public interface IPlatformPool
	{
		GameObject Get();
		void Return(GameObject obj);
	}
}
