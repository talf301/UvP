using System;
using Artemis.Interface;
namespace UvpRework
{
	public class BattleInfo : IComponent
	{
		private int power, speed, health, defense, projSpeed, rechargeTime, currentHealth, locX, locY, pTime;
		private bool ranged;
		private Team team;
		public BattleInfo (int power, int speed, int health, int defense, int projSpeed, int rechargeSpeed, bool ranged, Team team)
		{
			this.power = power;
			this.speed = speed;
			this.health = health;
			this.defense = defense;
			this.projSpeed = projSpeed;
			this.rechargeTime = rechargeSpeed;
			this.currentHealth = health;
			this.ranged = ranged;
			this.locX = 0;
			this.locY = 0;
			this.team = team;
		}

		public bool IsDead()
		{
			return health <= 0;
		}

		//Updates values to reflect a new battle beginning
		public void ResetBattle()
		{
			switch(team) {
				case Team.UPHOLDERS:
					locX = 30;
					locY = 300;
					break;
				case Team.PERSECUTORS:
					locX = 720;
					locY = 300;
					break;
			}
			this.pTime = rechargeTime;
		}
	}
}

