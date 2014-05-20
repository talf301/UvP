using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UvpRework
{
	public interface IGameState
	{
		void Initialize ();
		void LoadContent ();
		void Update(GameTime gameTime);
		void Draw (GameTime gameTime, SpriteBatch sb);
	}
}

