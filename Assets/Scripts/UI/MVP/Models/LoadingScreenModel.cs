using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MVP.Models
{
	public class LoadingScreenModel
	{
		public IEnumerator LoadSceneAsync(string sceneName, Action<float> onProgressUpdated, Action onComplete)
		{
			var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
			asyncOperation.allowSceneActivation = false;

			while (!asyncOperation.isDone)
			{
				float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
				onProgressUpdated?.Invoke(progress);

				if (asyncOperation.progress >= 0.9f)
				{
					onComplete?.Invoke();
					asyncOperation.allowSceneActivation = true;
				}

				yield return null;
			}
		}
	}
}
