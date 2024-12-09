using UnityEngine;

namespace Utils
{
	public interface IEnvironmentPlatformPool
	{
		GameObject Get();
		void Return(GameObject obj);
	}
}
