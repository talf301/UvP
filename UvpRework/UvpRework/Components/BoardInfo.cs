using System;
using Artemis.Interface;

namespace UvpRework
{
	public class BoardInfo : IComponent
	{
		private int movement, posX, posY;
		private bool flying;
		private Team team;
		public BoardInfo (int movement, bool flying, Team team, int initX, int initY)
		{
			this.movement = movement;
			this.flying = flying;
			this.team = team;
			this.posX = initX;
			this.posY = initY;
		}

		public bool IsFlying() { return flying; }
		public Team GetTeam() { return team; }
		public int GetMovement() { return movement; }
		public int getX() { return posX; }
		public int getY() { return posY; }
	}
}

