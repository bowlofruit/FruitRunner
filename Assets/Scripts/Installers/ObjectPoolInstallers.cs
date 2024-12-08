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
		[SerializeField] private int _prefabCount;

		[Header("Fruits")]
		[SerializeField] private GameObject[] _fruitPrefabs;
		[SerializeField] private Transform _fruitParent;
		[SerializeField] private int _fruitPrefabCount;

		[Header("Obstacles")]
		[SerializeField] private GameObject[] _obstaclePrefabs;
		[SerializeField] private Transform _obstacleParent;
		[SerializeField] private int _obstaclePrefabCount;

		public override void InstallBindings()
		{
			BindPathSegmentPool();
			BindFruitPools();
			BindObstaclePools();
		}

		private void BindFruitPools()
		{
			foreach (var prefab in _fruitPrefabs)
			{
				var fruitPool = new GameObjectPool(Container, prefab, _fruitPrefabCount, _fruitParent);
				Container.Bind<GameObjectPool>().WithId("Fruit").FromInstance(fruitPool).AsSingle().NonLazy();
			}
		}

		private void BindObstaclePools()
		{
			foreach (var prefab in _obstaclePrefabs)
			{
				var obstaclePool = new GameObjectPool(Container, prefab, _obstaclePrefabCount, _obstacleParent);
				Container.Bind<GameObjectPool>().WithId("Obstacle").FromInstance(obstaclePool).AsSingle().NonLazy();
			}
		}

		private void BindPathSegmentPool()
		{
			var pathSegmentPool = new GameObjectPool(Container, _platformPrefab, _prefabCount, _platformParent.transform);
			Container.Bind<GameObjectPool>().WithId("PathSegment").FromInstance(pathSegmentPool).AsSingle().NonLazy();
		}
	}
}