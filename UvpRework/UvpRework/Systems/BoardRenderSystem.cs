using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Artemis;
using Artemis.System;
using Artemis.Manager;
namespace UvpRework
{
	[Artemis.Attributes.ArtemisEntitySystem(ExecutionType = Artemis.Manager.ExecutionType.Asynchronous, Layer = 1)]
	public class BoardRenderSystem : EntityProcessingSystem
	{
		private SpriteBatch sb;
		private Texture2D Background, Board;
		public BoardRenderSystem() : base(Aspect.All(typeof(BoardInfo), typeof(Sprite))){
			sb = Game1.GetInstance ().GetSB ();
		}
		
		public override void LoadContent()
		{
			ContentManager Content = Game1.GetInstance().GetContent();
			Background = Content.Load<Texture2D>(Path.Combine("Sprites", "BoardBackground.png"));	
			Board = Content.Load<Texture2D>(Path.Combine("Sprites", "BoardNeutral"));
		}
		
		public override void Process()
		{
			sb.Draw(Background, new Rectangle(0,0,Background.Width,Background.Height), Color.White);
			sb.Draw(Board, new Rectangle(130,5,Board.Width,Board.Height), Color.White);
		}

		public override void Process (Entity e)
		{
			int x = e.GetComponent<BoardInfo>().getX();
			int y = e.GetComponent<BoardInfo>().getY();
			Texture2D sprite = e.GetComponent<Sprite>().GetBoardImage(e.GetComponent<BoardInfo>().GetTeam());
			sb.Draw(sprite, new Rectangle(135 + (60 * x), (8 - y) * 60 + 10, sprite.Width, sprite.Height), Color.White);  

		}
	}
}

