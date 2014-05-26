using System;
using System.IO;
using Artemis;
namespace UvpRework
{
	[Artemis.Attributes.ArtemisEntityTemplate("CharacterTemplate")]
	public class CharacterTemplate : Artemis.Interface.IEntityTemplate
	{
		public Entity BuildEntity(Entity e, EntityWorld eWorld, params object[] args)
		{
			TextReader tr = new StreamReader ((String)args [0]);
			int movement = int.Parse(tr.ReadLine());
			int power = int.Parse(tr.ReadLine());
			int speed = int.Parse(tr.ReadLine());
			int health = int.Parse(tr.ReadLine());
			int defense = int.Parse(tr.ReadLine());
			int projSpeed = int.Parse(tr.ReadLine());
			int rechargeSpeed = int.Parse(tr.ReadLine());
			bool ranged = parseBool(tr.ReadLine());
			bool flying = parseBool(tr.ReadLine());
			Team team = parseTeam(tr.ReadLine());
			String spriteLocation = tr.ReadLine();
			e.AddComponent (new BattleInfo (power, speed, health, defense, projSpeed, rechargeSpeed, ranged));
			e.AddComponent (new BoardInfo (movement, flying, Team));
			e.AddComponent (new Sprite (spriteLocation));
		}

		private bool parseBool(String input)
		{
			if (input.Equals("True") || input.Equals("true"))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private Team parseTeam(String input)
		{
			if (parseBool (input))
				return Team.PERSECUTORS;
			else
				return Team.UPHOLDERS;
		}

	}
}

