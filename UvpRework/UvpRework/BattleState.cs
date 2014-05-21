using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace UvpRework
{
	public class BattleState : IGameState
	{
		public BattleState ()
		{
		}

		public void Initialize(){}

		public void LoadContent(ContentManager Content)
		{
		}

		public void Update(GameTime gameTime)
		{
		}

		public void Draw(GameTime gameTime, SpriteBatch sb)
		{
			sb.GraphicsDevice.Clear (Color.CornflowerBlue);
		}
	}
}

