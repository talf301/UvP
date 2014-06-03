#region File Description
//-----------------------------------------------------------------------------
// UvpReworkGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Artemis.System;

#endregion
namespace UvpRework
{
	/// <summary>
	/// Default Project Template
	/// </summary>
	public class Game1 : StateManagedGame
	{
		#region Fields
		public static int BOARD_STATE, BATTLE_STATE;
		private static Game1 instance;
		#endregion

		#region Initialization

		public Game1 ()
		{
			
			Content.RootDirectory = "Content";
			BOARD_STATE = base.Add (BoardState.GetInstance ());
			BATTLE_STATE = base.Add (BattleState.GetInstance ());
			EntitySystem.BlackBoard.SetEntry<ContentManager>("Content", Content);
			EntitySystem.BlackBoard.SetEntry<SpriteBatch>("SpriteBatch", sb);
			EntitySystem.BlackBoard.SetEntry<GraphicsDeviceManager>("Graphics", graphics);	
		}

		public static Game1 GetInstance()
		{
			if (instance == null)
				instance = new Game1 ();
			return instance;
		}
		/// <summary>
		/// Load your graphics content.
		/// </summary>
		protected override void LoadContent ()
		{
			base.LoadContent ();
		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// TODO: Add your update logic here			
			if (Keyboard.GetState ().IsKeyDown (Keys.A)) {
				base.ChangeState (1);	
			}
			base.Update (gameTime);
		}

		#endregion
	}
}
