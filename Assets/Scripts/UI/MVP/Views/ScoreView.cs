using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace MVP.Views
{
	public class ScoreView : MonoBehaviour, IScoreView
	{
		[SerializeField] private TMP_Text scoreText;

		public void SetScoreText(string score)
		{
			scoreText.text = $"Score: {score}";
		}
	}
}