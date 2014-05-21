using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace UvpRework
{
	public class BoardState : IGameState
	{
		Texture2D logoTexture;
		public BoardState ()
		{
		}

		public void Initialize()
		{
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

