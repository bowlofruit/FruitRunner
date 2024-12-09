using UnityEngine;
using UnityEngine.UI;

namespace MVP.Views
{
	public class LoadingScreenView : MonoBehaviour, ILoadingScreenView
	{
		[SerializeField] private Slider progressBar;
		[SerializeField] private GameObject loadingScreenCanvas;

		public void Show()
		{
			loadingScreenCanvas.SetActive(true);
		}

		public void Hide()
		{
			loadingScreenCanvas.SetActive(false);
		}

		public void UpdateProgress(float progress)
		{
			progressBar.value = progress;
		}
	}
}