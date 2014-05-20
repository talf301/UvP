using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UvpRework
{
	public abstract class GameState
	{
		public GameState ()
		{
		}

		public void Initialize(){
		}
		public void LoadContent() {
		}
		public void Update(GameTime gameTime) {
		}

		public void Draw(GameTime gameTime, SpriteBatch sb){
		}


	}
}

