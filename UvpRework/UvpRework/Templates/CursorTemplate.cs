using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Artemis;
namespace UvpRework
{
	[Artemis.Attributes.ArtemisEntityTemplate("CursorTemplate")]
	public class CursorTemplate : Artemis.Interface.IEntityTemplate
	{
		public Entity BuildEntity(Entity e, EntityWorld eWorld, params object[] args)
		{
			e.AddComponent(new CursorInfo());
			ContentManager Content = Game1.GetInstance().GetContent();
			Texture2D[] sprites = new Texture2D[8];
			//We load in 4 copies of the sprite to slightly cheat the sprite system
			for(int i = 0; i < 4; i++)
			{
				sprites[i] = Content.Load<Texture2D> ("Sprites" + Path.DirectorySeparatorChar + "PersecutorsCursor");
			}

			//Do it again
			for(int i = 4; i < 8; i++)
			{
				sprites[i] = Content.Load<Texture2D> ("Sprites" + Path.DirectorySeparatorChar + "UpholdersCursor");
			}
			e.AddComponent(new Sprite(sprites));
			e.Tag = "Cursor";
			return e;			
		}
	}



}
