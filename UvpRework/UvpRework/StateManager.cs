/*This class is a simple state maanager for dealing with games with multiple states
 * i.e. menus, different screens, etc. Each of your states should be a child of GameState
 * and your main game class should have a single StateManager to deal with all of this.
 * Written By Tal Friedman
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace UvpRework
{
	public class StateManager
	{
		private List<GameState> states;
		private GameState activeState;

		//Add returns the index in the list of your added state, i.e. the StateID
		public int Add(GameState gs)
		{
			states.Add (gs);
			return states.IndexOf (gs);
		}

		public void ChangeState(int index)
		{
			try{
				activeState = states[index];
			}catch(IndexOutOfRangeException e) {
				Console.WriteLine (e.StackTrace);
			}
		}

		public void Initialize()
		{
			foreach (GameState gs in states) 
			{
				gs.Initialize;
			}
		}

		public void LoadContent()
		{
			foreach (GameState gs in states) 
			{
				gs.Initialize;
			}
		}

		public void Update(GameTime gameTime)
		{
			activeState.Update (gameTime);
		}
	}
}

