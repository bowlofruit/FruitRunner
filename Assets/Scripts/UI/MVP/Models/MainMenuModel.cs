using UnityEngine;

namespace MVP.Models
{
	public class MainMenuModel
	{
		public void StartGame()
		{
			// Логіка запуску гри (можливо, додатково перевірити стан гри)
		}

		public void ShowLeaderboard()
		{
			// Логіка відображення таблиці лідерів
		}

		public void ExitGame()
		{
			// Логіка виходу з гри
			Application.Quit();
		}
	}
}
