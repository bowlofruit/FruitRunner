using MVP.Models;
using MVP.Views;
using System.Collections;
using UnityEngine;

namespace MVP.Presenters
{
	public class LoadingScreenPresenter
	{
		private readonly LoadingScreenModel _model;
		private readonly ILoadingScreenView _view;

		public LoadingScreenPresenter(ILoadingScreenView view)
		{
			_model = new LoadingScreenModel();
			_view = view;
			_view.Hide();
		}

		public void StartLoading(string sceneName, MonoBehaviour monoBehaviour, float delay = 2f)
		{
			_view.Show();
			monoBehaviour.StartCoroutine(LoadSceneWithDelay(sceneName, monoBehaviour, delay));
		}

		private IEnumerator LoadSceneWithDelay(string sceneName, MonoBehaviour monoBehaviour, float delay)
		{
			yield return monoBehaviour.StartCoroutine(_model.LoadSceneAsync(
				sceneName,
				progress => _view.UpdateProgress(progress),
				() => _ = true
			));

			SystemInstaller.TimeFreezer?.Invoke(true);

			float timer = 0f;
			while (timer < delay)
			{
				timer += Time.deltaTime;
				yield return null;
			}

			_view.Hide();

			SystemInstaller.TimeFreezer?.Invoke(false);
		}
	}
}