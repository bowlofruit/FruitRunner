using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Utils
{
	public class GameObjectPool
	{
		private readonly Stack<GameObject> _pool;
		private readonly GameObject _prefab;
		private readonly Transform _parent;
		private readonly DiContainer _container;

		public GameObjectPool(DiContainer container, GameObject prefab, int initialCapacity = 10, Transform parent = null)
		{
			_prefab = prefab;
			_parent = parent;
			_pool = new Stack<GameObject>(initialCapacity);
			_container = container;

			for (int i = 0; i < initialCapacity; i++)
			{
				var obj = _container.InstantiatePrefab(_prefab, _parent.position, Quaternion.identity, _parent);
				obj.SetActive(false);
				_pool.Push(obj);
			}
		}

		public GameObject Get()
		{
			if (_pool.Count > 0)
			{
				var obj = _pool.Pop();
				obj.SetActive(true);
				return obj;
			}

			return _container.InstantiatePrefab(_prefab, _parent.position, Quaternion.identity, _parent);
		}

		public void Return(GameObject obj)
		{
			obj.SetActive(false);
			_pool.Push(obj);
		}
	}
}
