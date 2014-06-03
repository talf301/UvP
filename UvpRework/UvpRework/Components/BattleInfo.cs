using System;
using Artemis.Interface;
namespace UvpRework
{
	public class BattleInfo : IComponent
	{
		private int power, speed, health, defense, projSpeed, rechargeSpeed, currentHealth;
		private bool ranged;
		public BattleInfo (int power, int speed, int health, int defense, int projSpeed, int rechargeSpeed, bool ranged)
		{
			this.power = power;
			this.speed = speed;
			this.health = health;
			this.defense = defense;
			this.projSpeed = projSpeed;
			this.rechargeSpeed = rechargeSpeed;
			this.currentHealth = health;
			this.ranged = ranged;
		}

		public bool IsDead()
		{
			return health <= 0;
		}
	}
}

