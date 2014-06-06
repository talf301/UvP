using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace UvpRework
{
	public class StateManagedGame : Game
	{
		#region Fields
		private List<IGameState> states;
		IGameState activeState;
		protected SpriteBatch sb;
		protected GraphicsDeviceManager graphics;
		#endregion

		#region Initialization
		public StateManagedGame ()
		{
			graphics = new GraphicsDeviceManager (this);
			states = new List<IGameState> ();
		}

		protected override void Initialize()
		{
			sb = new SpriteBatch (GraphicsDevice);
			foreach (IGameState s in states)
				s.Initialize();
			try{
				activeState = states[0];
			} catch(IndexOutOfRangeException e) {
				Console.WriteLine("No states set");
				Console.WriteLine (e.StackTrace);
			}
			base.Initialize ();
		}

		protected override void LoadContent()
		{
			foreach (IGameState s in states)
				s.LoadContent ();
			base.LoadContent ();
		}
		#endregion

		#region Getters
		public SpriteBatch GetSB() {
			return sb;
		}

		public ContentManager GetContent() {
			return Content;
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
			sb.Begin ();

			activeState.Draw (gameTime);

			base.Draw (gameTime);

			sb.End ();
		}
		#endregion

		#region State logic
		//Returned number is the index in the array, effectively the stateID
		protected int Add(IGameState gs)
		{
			states.Add (gs);
			return states.Count - 1;
		}

		public void ChangeState(int index)
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

