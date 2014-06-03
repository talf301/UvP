using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Artemis.Interface;
using Artemis.System;
using Artemis.Blackboard;
namespace UvpRework
{
	public class Sprite : IComponent
	{
		private Texture2D[] sprites;
		public Sprite (Texture2D[] sprites)
		{
			this.sprites = sprites;
		}

		public Texture2D GetBoardImage(Team team)
		{
			switch(team) {
			case Team.PERSECUTORS:
				return sprites [2];
			case Team.UPHOLDERS:
				return sprites [6];
			}
			return null;	
		}
	}
}

