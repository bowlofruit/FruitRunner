using UnityEngine;
using Utils;
using Zenject;

namespace Installers
{
	public class ObjectPoolInstallers : MonoInstaller
	{
		[SerializeField] private GameObject _platformPrefab;
		[SerializeField] private Transform _platformParent;
		[SerializeField] private int _prefabCount;

		public override void InstallBindings()
		{
			BindPathSegmentPool();
		}

		private void BindPathSegmentPool()
		{
			var pathSegmentPool = new GameObjectPool(Container, _platformPrefab, _prefabCount, _platformParent.transform);
			Container.Bind<GameObjectPool>().FromInstance(pathSegmentPool).AsSingle().NonLazy();
		}
	}
}