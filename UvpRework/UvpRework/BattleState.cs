using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace UvpRework
{
	public class BattleState : IGameState
	{
		private static BattleState instance;
		private SpriteBatch sb;
		private ContentManager Content;
		public BattleState ()
		{
		}

		public static BattleState GetInstance()
		{
			if (instance == null)
				instance = new BattleState ();
			return instance;
		}

		public void Initialize(){
			sb = Game1.GetInstance().GetSB();
			Content = Game1.GetInstance().GetContent();
		}

		public void LoadContent()
		{
		}

		public void Update(GameTime gameTime)
		{
		}

		public void Draw(GameTime gameTime)
		{
			sb.GraphicsDevice.Clear (Color.CornflowerBlue);
		}
	}
}

