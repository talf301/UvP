using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace UvpRework
{
	public class BattleState : IGameState
	{
		private static BattleState instance;
		public BattleState ()
		{
		}

		public BattleState GetInstance()
		{
			if (instance == null)
				instance = new BattleState ();
			return instance;
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

