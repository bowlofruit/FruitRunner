using UnityEngine;

namespace Utils
{
	public interface IEnviromentObjectsPool
	{
		GameObject Get();
		void Return(GameObject obj);
	}
}
