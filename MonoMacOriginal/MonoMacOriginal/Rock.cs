using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoMacOriginal
{
	public class Rock
	{
		Texture2D sprite;
		public int x, y;

		public Rock(Game game)
		{
			sprite = game.Content.Load<Texture2D>("Sprites/RockGray");
		}

		public void DrawRock(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(sprite, new Rectangle(x, y, sprite.Width, sprite.Height), Color.White);
		}
	}
}
