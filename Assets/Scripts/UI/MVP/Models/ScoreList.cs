using System.Collections.Generic;

namespace MVP.Models
{
	public partial class LeaderboardModel
	{
		[System.Serializable]
		private class ScoreList
		{
			public List<ScoreEntry> Entries;
		}
	}
}
