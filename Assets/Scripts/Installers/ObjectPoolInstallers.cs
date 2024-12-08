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

			BindPlatformPool();
			BindFruitPool();
			BindObstaclePool();
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
