using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Utils
{
	public class GameObjectPool : IFruitPool, IObstaclePool, IPlatformPool
	{
		private readonly Stack<GameObject> _pool;
		private readonly GameObject[] _prefabs;
		private readonly Transform _parent;
		private readonly DiContainer _container;

		public GameObjectPool(DiContainer container, GameObject[] prefabs, int initialCapacity = 10, Transform parent = null)
		{
			_prefabs = prefabs;
			_parent = parent;
			_pool = new Stack<GameObject>(initialCapacity);
			_container = container;

			Debug.Log($"GameObjectPool initialized with {initialCapacity} objects.");

			InitObjects(initialCapacity);
		}

		public void InitObjects(int initialCapacity)
		{
			for (int i = 0; i < initialCapacity; i++)
			{
				var obj = CreateNewInstance();
				obj.SetActive(false);
				_pool.Push(obj);
				Debug.Log($"Created new instance of prefab and added to pool: {obj.name}");
			}
		}

		private GameObject CreateNewInstance()
		{
			var prefab = _prefabs[Random.Range(0, _prefabs.Length)];
			var instance = _container.InstantiatePrefab(prefab, _parent.position, Quaternion.identity, _parent);
			return instance;
		}

		public GameObject Get()
		{
			if (_pool.Count > 0)
			{
				var obj = _pool.Pop();
				obj.SetActive(true);
				Debug.Log($"Object retrieved from pool: {obj.name}");
				return obj;
			}

			Debug.LogWarning("Pool is empty, creating a new instance.");
			return CreateNewInstance();
		}

		public void Return(GameObject obj)
		{
			obj.SetActive(false);
			_pool.Push(obj);
			Debug.Log($"Object returned to pool: {obj.name}");
		}
	}
}
