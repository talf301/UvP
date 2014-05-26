using System;
using Artemis.Interface;

namespace UvpRework
{
	public class BoardInfo : IComponent
	{
		private int movement { get; }
		private bool flying;
		public Team team;
		public BoardInfo (int movement, bool flying, Team team)
		{
		}
	}
}

