using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Artemis;
using Artemis.System;
using Artemis.Manager;

namespace UvpRework
{
	[Artemis.Attributes.ArtemisEntitySystem(ExecutionType = Artemis.Manager.ExecutionType.Synchronous, GameLoopType = GameLoopType.Draw, Layer = 1)]
	public class BoardRenderSystem : ProcessingSystem{
		private SpriteBatch sb;
		private Texture2D Background, Board;
		public BoardRenderSystem() : base()
		{
			sb = Game1.GetInstance().GetSB();
		}

		public override void LoadContent()
		{
			ContentManager Content = Game1.GetInstance().GetContent();
			Background = Content.Load<Texture2D>(Path.Combine("Sprites", "BoardBackground.png"));	
			Board = Content.Load<Texture2D>(Path.Combine("Sprites", "BoardNeutral"));
		}
		
		public override void ProcessSystem()
		{
			sb.Draw(Background, new Rectangle(0,0,Background.Width,Background.Height), Color.White);
			sb.Draw(Board, new Rectangle(130,5,Board.Width,Board.Height), Color.White);
		}

	}
}
