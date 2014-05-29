using System;
using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Artemis;
namespace UvpRework
{
	[Artemis.Attributes.ArtemisEntityTemplate("CharacterTemplate")]
	public class CharacterTemplate : Artemis.Interface.IEntityTemplate
	{
		//Expecting args[0] to be the location of the descriptive file, args[1] to be ContentManager
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
			bool ranged = ParseBool(tr.ReadLine());
			bool flying = ParseBool(tr.ReadLine());
			Team team = ParseTeam(tr.ReadLine());
			String spriteLocation = tr.ReadLine();

			Texture2D[] sprites;
			ContentManager Content = (ContentManager)args [1];
			if (ranged) {
				sprites = new Texture2D[8];
				for (int i = 0; i < 8; i++)
					sprites [i] = Content.Load<Texture2D> (spriteLocation + Path.PathSeparator + i);
			} else {
				sprites = new Texture2D[16];
				for(int i = 0; i < 16; i++)
					sprites [i] = Content.Load<Texture2D> (spriteLocation + Path.PathSeparator + i);
			}
			e.AddComponent (new BattleInfo (power, speed, health, defense, projSpeed, rechargeSpeed, ranged));
			e.AddComponent (new BoardInfo (movement, flying, Team));
			e.AddComponent (new Sprite (sprites));
		}

		private bool ParseBool(String input)
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

		private Team ParseTeam(String input)
		{
			if (ParseBool (input))
				return Team.PERSECUTORS;
			else
				return Team.UPHOLDERS;
		}

	}
}

