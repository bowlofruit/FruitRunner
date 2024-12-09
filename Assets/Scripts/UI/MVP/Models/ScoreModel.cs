using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MVP.Models
{
	public class ScoreModel
    {
        public int Score { get; private set; }

        public void SetScore(int score)
        {
            Score = score;
        }

        public void AddScore(int amount)
        {
            Score += amount;
        }
	}
}
