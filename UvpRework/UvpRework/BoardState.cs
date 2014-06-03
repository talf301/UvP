using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Artemis;
using Artemis.Blackboard;
namespace UvpRework
{
	public class BoardState : IGameState
	{
		EntityWorld world;
		Texture2D logoTexture;
		private static BoardState instance;
		public BoardState ()
		{
			
		}

		public static BoardState GetInstance()
		{
			if (instance == null)
				instance = new BoardState ();
			return instance;
		}

		public void Initialize()
		{
			world = new EntityWorld ();
		}

		public void LoadContent(ContentManager Content)
		{
			logoTexture = Content.Load<Texture2D> ("logo");
		}

		public void Update(GameTime gameTime)
		{

		}

		public void Draw(GameTime gameTime, SpriteBatch sb)
		{
			sb.Draw (logoTexture, new Vector2 (130, 200), Color.White);
		}

	}
}

