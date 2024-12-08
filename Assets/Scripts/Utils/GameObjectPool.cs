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

		public GameObjectPool(DiContainer content, GameObject prefab, int initialCapacity = 10, Transform parent = null)
		{
			_prefab = prefab;
			_parent = parent;
			_pool = new Stack<GameObject>(initialCapacity);
			_container = content;

			for (int i = 0; i < initialCapacity; i++)
			{
				GameObject obj = _container.InstantiatePrefab(_prefab, _parent.position, Quaternion.identity, _parent);
				obj.SetActive(false);
				_pool.Push(obj);
			}
		}

		public GameObject Get()
		{
			if (_pool.Count > 0)
			{
				GameObject obj = _pool.Pop();
				obj.SetActive(true);
				return obj;
			}
			else
			{
				GameObject obj = _container.InstantiatePrefab(_prefab, _parent);
				return obj;
			}
		}

		public void Return(GameObject obj)
		{
			obj.SetActive(false);
			_pool.Push(obj);
		}
	}
}
