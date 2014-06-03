using System;
using Microsoft.Xna.Framework;
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
		public BoardRenderSystem() : base(Aspect.All(typeof(BoardInfo), typeof(Sprite))){
			sb = EntitySystem.BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
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

