using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UvpRework
{
	public class StateManagedGame : Game
	{
		#region Fields
		private List<IGameState> states;
		IGameState activeState;
		SpriteBatch sb;
		GraphicsDeviceManager graphics;
		#endregion

		#region Initialization
		public StateManagedGame ()
		{
			graphics = new GraphicsDeviceManager (this);
		}

		protected override void Initialize()
		{
			foreach (IGameState s in states)
				s.Initialize;
			base.Initialize ();
		}

		protected override void LoadContent()
		{
			sb = new SpriteBatch (graphics.GraphicsDevice);
			foreach (IGameState s in states)
				s.LoadContent ();
		}
		#endregion

		#region Update+Draw
		protected override void Update(GameTime gameTime)
		{
			activeState.Update (gameTime);
			base.Update (gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			foreach (IGameState s in states)
				s.Draw (gameTime, sb);
			base.Draw (gameTime);
		}
		#endregion

		#region State logic
		//Returned number is the index in the array, effectively the stateID
		protected int Add(IGameState gs)
		{
			states.Add (gs);
			return states.Count - 1;
		}

		protected void ChangeState(int index)
		{
			try{
				activeState = states[index];
			}catch(IndexOutOfRangeException e){
				Console.WriteLine (e.StackTrace);
			}
		}
		#endregion
	}
}

