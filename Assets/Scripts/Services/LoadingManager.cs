using MVP.Presenters;
using UnityEngine;
using Zenject;

namespace Services
{
	public class LoadingManager : MonoBehaviour
	{
		private LoadingScreenPresenter _loadingScreenPresenter;

		[Inject]
		private void Construct(LoadingScreenPresenter loadingScreenPresenter)
		{
			_loadingScreenPresenter = loadingScreenPresenter;
		}

		public void LoadScene(string sceneName)
		{
			_loadingScreenPresenter.StartLoading(sceneName, this);
		}
	}
}