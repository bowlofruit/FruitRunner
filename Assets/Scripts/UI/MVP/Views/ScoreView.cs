using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using ECS.Components;

namespace MVP.Views
{
	public class ScoreView : MonoBehaviour, IScoreView
	{
		[SerializeField] private TMP_Text scoreText;
		[SerializeField] private TMP_Text fruitCountsText;

		public void SetScoreText(string score)
		{
			scoreText.text = score;
		}

		public void SetFruitCounts(Dictionary<FruitType, int> fruitCounts)
		{
			string countsDisplay = "Fruits Collected:\n";
			foreach (var pair in fruitCounts)
			{
				countsDisplay += $"{pair.Key}: {pair.Value}\n";
			}

			fruitCountsText.text = countsDisplay;
		}
	}
}