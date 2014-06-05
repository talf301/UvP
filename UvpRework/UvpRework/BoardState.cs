using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Artemis;
using Artemis.Manager;
using Artemis.System;
using Artemis.Blackboard;
namespace UvpRework
{
	public class BoardState : IGameState
	{
		private EntityWorld world;
		private EntitySystem BoardInputSystem;
		private SystemManager Sys;
		Texture2D logoTexture;
		private Entity[][] State;
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
			Sys = world.SystemManager;
			world.InitializeAll(true);
					}

		public void LoadContent(ContentManager Content)
		{
			logoTexture = Content.Load<Texture2D> ("logo");
			Entity e = world.CreateEntity();
			e.AddComponent(new BoardInfo(0,false,Team.UPHOLDERS,5,5));
			Texture2D[] t = new Texture2D[8];
			for(int i = 0; i < 8; i++)
			{
				t[i] = logoTexture;
			}
			e.AddComponent(new Sprite(t));
			e.Refresh();
					
		}

		public void Update(GameTime gameTime)
		{
		}

		public void Draw(GameTime gameTime, SpriteBatch sb)
		{
			Sys.GetSystem<BoardRenderSystem>()[0].Process();
			sb.Draw (logoTexture, new Vector2 (130, 200), Color.White);
		}

	}
}

