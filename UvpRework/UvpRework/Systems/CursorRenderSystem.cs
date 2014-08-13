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
	[Artemis.Attributes.ArtemisEntitySystem(ExecutionType = Artemis.Manager.ExecutionType.Synchronous, GameLoopType = GameLoopType.Draw, Layer = 2)]
	public class CursorRenderSystem : TagSystem
	{
		private SpriteBatch sb;
		public CursorRenderSystem() : base("Cursor")
		{
			sb = Game1.GetInstance().GetSB();
		}

		//Loading arrows and whatnot goes here
		public override void LoadContent()
		{
			ContentManager Content = Game1.GetInstance().GetContent();
		}

		public override void Process(Entity e)
		{
			int x = e.GetComponent<CursorInfo>().X;
			int y = e.GetComponent<CursorInfo>().Y;
			Texture2D sprite = e.GetComponent<Sprite>().GetBoardImage(e.GetComponent<CursorInfo>().team);
			sb.Draw(sprite, new Rectangle(125 + (x - 1) * 60, (9 - y) * 60, sprite.Width, sprite.Height), Color.White);

		}
	}

}
