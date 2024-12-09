using UnityEngine;
using Utils;
using Zenject;

namespace Installers
{
	public class ObjectPoolInstallers : MonoInstaller
	{
		[Header("Platform")]
		[SerializeField] private GameObject _platformPrefab;
		[SerializeField] private Transform _platformParent;
		[SerializeField] private int _platformPoolCount;

		[Header("Environment")]
		[SerializeField] private GameObject _environmentPrefab;
		[SerializeField] private Transform _environmentParent;
		[SerializeField] private int _environmentPoolCount;

		[Header("EnvironmentObjects")]
		[SerializeField] private GameObject[] _environmentObjectsPrefabs;
		[SerializeField] private Transform _environmentObjectsParent;
		[SerializeField] private int _environmentObjectsPoolCount;

		[Header("Fruits")]
		[SerializeField] private GameObject[] _fruitPrefabs;
		[SerializeField] private Transform _fruitParent;
		[SerializeField] private int _fruitPoolCount;

		[Header("Obstacles")]
		[SerializeField] private GameObject[] _obstaclePrefabs;
		[SerializeField] private Transform _obstacleParent;
		[SerializeField] private int _obstaclePoolCount;

		public override void InstallBindings()
		{
			Debug.Log("Installing object pools...");

			BindEnvironmentPlatformPool();
			BindEnvironmentObjectsPlatformPool();
			BindPlatformPool();
			BindFruitPool();
			BindObstaclePool();
		}

		private void BindEnvironmentPlatformPool()
		{
			var environmentPlatformPool = new GameObjectPool(Container, new[] { _environmentPrefab }, _environmentPoolCount, _environmentParent);
			Container.Bind<IEnvironmentPlatformPool>().FromInstance(environmentPlatformPool).AsSingle();
			Debug.Log("Platform pool bound.");
		}

		private void BindEnvironmentObjectsPlatformPool()
		{
			var environmentPlatformObjectsPool = new GameObjectPool(Container, _environmentObjectsPrefabs, _environmentObjectsPoolCount, _environmentObjectsParent);
			Container.Bind<IEnviromentObjectsPool>().FromInstance(environmentPlatformObjectsPool).AsSingle();
			Debug.Log("Platform pool bound.");
		}


		private void BindPlatformPool()
		{
			var platformPool = new GameObjectPool(Container, new[] { _platformPrefab }, _platformPoolCount, _platformParent);
			Container.Bind<IPlatformPool>().FromInstance(platformPool).AsSingle();
			Debug.Log("Platform pool bound.");
		}

		private void BindFruitPool()
		{
			var fruitPool = new GameObjectPool(Container, _fruitPrefabs, _fruitPoolCount, _fruitParent);
			Container.Bind<IFruitPool>().FromInstance(fruitPool).AsSingle();
			Debug.Log("Fruit pool bound.");
		}

		private void BindObstaclePool()
		{
			var obstaclePool = new GameObjectPool(Container, _obstaclePrefabs, _obstaclePoolCount, _obstacleParent);
			Container.Bind<IObstaclePool>().FromInstance(obstaclePool).AsSingle();
			Debug.Log("Obstacle pool bound.");
		}
	}
}
