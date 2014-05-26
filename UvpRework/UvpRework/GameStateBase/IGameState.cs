using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace UvpRework
{
	public interface IGameState
	{
		void Initialize ();
		void LoadContent (ContentManager Content);
		void Update(GameTime gameTime);
		void Draw (GameTime gameTime, SpriteBatch sb);
	}
}

