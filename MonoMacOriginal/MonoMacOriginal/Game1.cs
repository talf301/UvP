using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace MonoMacOriginal
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		//SpriteFont gameOverFont;
		KeyboardState previousKeyboardState;
		const Boolean UPHOLDERS = true, PERSECUTORS = false;
		static Boolean battleOn = false;
		static int uPieceCount = 18, pPieceCount = 18;
		GraphicsDeviceManager graphics;
		//AudioEngine audioEngine;
		public static SoundBank soundBank;
		//WaveBank waveBank;
		SpriteBatch spriteBatch;
		Rectangle viewportRect;
		bool gameOver = false, winner;
		Texture2D upholdersWin, persecutorsWin;
		//Characters, battles, and cursors
		Character[] assassin;
		Character athas;
		Character blob;
		Character blobasaur;
		Character[] callivion;
		Character[] dragonRider;
		Character[] druid;
		Character einar;
		Character[] gauntler;
		Character[] knight;
		Character[] lion;
		Character[] mage;
		Character[] marksmen;
		Character phoenix;
		Character shadower;
		Character[] soldier;
		Character[] valkyrie;
		Character[] wyvern;
		public static Cursor cursor;
		public static Board mainBoard;
		public static Battle mainBattle;
		public static Boolean activeTeam;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			//Rectangle represents viewed screen
			viewportRect = new Rectangle(0, 0, 800, 600);

			//Set dimensions to 600x800
			graphics.PreferredBackBufferHeight = 600;
			graphics.PreferredBackBufferWidth = 800;

			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			//audioEngine = new AudioEngine("Content/Sound/UvP.xgs");
			//waveBank = new WaveBank(audioEngine, "Content/Sound/Wave Bank.xwb");
			//soundBank = new SoundBank(audioEngine, "Content/Sound/Sound Bank.xsb");
			//gameOverFont = Content.Load<SpriteFont>("Fonts/GameOver");
			persecutorsWin = Content.Load<Texture2D>("Sprites/PersecutorsWin_1");
			upholdersWin = Content.Load<Texture2D>("Sprites/UpholdersWin_1");

			LoadCharacters();
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>d
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			//New keyboard state created each frame and used for all key related operations
			KeyboardState keyboardState = Keyboard.GetState();

			if (!gameOver)
			{
				//Update board if there is no battle
				if (!battleOn)
				{
					mainBoard.Update(keyboardState, previousKeyboardState, cursor, mainBoard, this);
				}
				else
				{
					mainBattle.Update(keyboardState, previousKeyboardState);
				}

				//F10 fullscreens the game
				if (keyboardState.IsKeyDown(Keys.F10))
				{
					graphics.ToggleFullScreen();
				}

				//Check if the game has been won
				CheckWin();
			}
			else
			{
				if(keyboardState.IsKeyDown(Keys.Enter))
				{


					mainBoard.boardMusic.Stop(AudioStopOptions.Immediate);
					mainBattle.fightMusic.Stop(AudioStopOptions.Immediate);
					LoadCharacters();
					gameOver = false;
					battleOn = false;
					uPieceCount = 18;
					pPieceCount = 18;

				}
			}

			//Turn old keyboard state into the current keyboard state for next frame
			previousKeyboardState = keyboardState;
			base.Update(gameTime);
		}

		//Method is essentially a collection of what must be done when board team changes
		public static void ToggleTeam(String team, Cursor cursor, Board board)
		{
			//Change team cursor
			cursor.ToggleUser(team);

			//Reassign all selectable squares according to team
			cursor.SetSelectableSquares(board.SelectableSquares(team));

			//Move the cursor to the appropriate team's starting location
			cursor.Reset(team);

			//Toggle the "Active Team" variable
			if (team.Equals("upholders"))
			{
				activeTeam = Character.UPHOLDERS;
			}
			else
			{
				activeTeam = Character.PERSECUTORS;
			}
		}

		//Method contains all actions which trigger battle
		public void TriggerBattle(Character piece1, Character piece2, int x, int y)
		{
			//Construct a new battle
			//Toggle battle mode to on
			mainBattle = new Battle(this, piece1, piece2, x, y, mainBoard.boardColours[cursor.x - 1, cursor.y - 1]);
			battleOn = true;
		}

		//Method contains all actions to end battle
		public static void EndBattle(Character victor, int x, int y, int colour)
		{
			//Reset attack
			if (victor.team == UPHOLDERS && colour == Board.BOARD_WHITE)
			{
				victor.power -= 5;
			}
			else if (victor.team == PERSECUTORS && colour == Board.BOARD_GREEN)
			{
				victor.power -= 5;
			}
			//Toggle battle mode off
			battleOn = false;

			//Capture square
			mainBoard.CaptureSpace(victor, x, y);
			//Fixes other team controlling unit bug
			if (activeTeam == PERSECUTORS)
				cursor.SetSelectableSquares(mainBoard.SelectableSquares("persecutors"));
			else
				cursor.SetSelectableSquares(mainBoard.SelectableSquares("upholders"));
			//Play dying sound effect :D
			if (victor.team == Character.PERSECUTORS)
			{
				//soundBank.PlayCue("DyingSound");
			}
			else
			{
				//soundBank.PlayCue("PersecutorsDeath");
			}

			//Pause battle music
			//mainBattle.fightMusic.Stop(AudioStopOptions.Immediate);
			//Start music again :D
			//mainBoard.boardMusic.Resume();

			//Reduce piece count
			if (victor.team == PERSECUTORS)
			{
				uPieceCount--;
			}
			else
			{
				pPieceCount--;
			}
		}

		//Method checks if the game is won
		public void CheckWin()
		{
			//Check if everyone is dead                                                                                        
			if (uPieceCount == 0)
			{
				gameOver = true;
				winner = PERSECUTORS;
				//mainBoard.boardMusic.Stop(AudioStopOptions.Immediate);
				//mainBattle.fightMusic = Game1.soundBank.GetCue("Persecutors Fight");
				//mainBattle.fightMusic.Play();
			}
			else if (pPieceCount == 0)
			{
				gameOver = true;
				winner = UPHOLDERS;
				//mainBoard.boardMusic.Stop(AudioStopOptions.Immediate);
				//mainBattle.fightMusic = Game1.soundBank.GetCue("Upholders Fight");
				//mainBattle.fightMusic.Play();
			}

			//Check if power points are held
			if (mainBoard.board[4, 8] != null && mainBoard.board[4, 4] != null && mainBoard.board[4, 0] != null && mainBoard.board[0,4] != null && mainBoard.board[8,4] != null)
			{
				//Persecutors
				if (mainBoard.board[4, 8].team == PERSECUTORS && mainBoard.board[4, 4].team == PERSECUTORS && mainBoard.board[4, 0].team == PERSECUTORS && mainBoard.board[0, 4].team == PERSECUTORS && mainBoard.board[8, 4].team == PERSECUTORS)
				{
					gameOver = true;
					winner = PERSECUTORS;
					//mainBoard.boardMusic.Stop(AudioStopOptions.Immediate);
					//mainBattle.fightMusic = Game1.soundBank.GetCue("Persecutors Fight");
					//mainBoard.boardMusic.Stop(AudioStopOptions.Immediate);
				}
				else if (mainBoard.board[4, 8].team == UPHOLDERS && mainBoard.board[4, 4].team == UPHOLDERS && mainBoard.board[4, 0].team == UPHOLDERS && mainBoard.board[0, 4].team == UPHOLDERS && mainBoard.board[8, 4].team == UPHOLDERS)
				{
					gameOver = true;
					winner = UPHOLDERS;
					//mainBoard.boardMusic.Stop(AudioStopOptions.Immediate);
					//mainBattle.fightMusic = Game1.soundBank.GetCue("Upholders Fight");
					//mainBattle.fightMusic.Play();
				}

			}

		}

		public void LoadCharacters()
		{
			assassin = new Character[2];
			for (int i = 0; i < 2; i++)
			{
				assassin[i] = new Character(this, "Assassin.txt");
			}
			athas = new Character(this, "Athas.txt");
			blob = new Character(this, "blob.txt");
			blobasaur = new Character(this, "blobasaur.txt");
			callivion = new Character[2];
			for (int i = 0; i < 2; i++)
			{
				callivion[i] = new Character(this, "Callivion.txt");
			}
			dragonRider = new Character[2];
			for (int i = 0; i < 2; i++)
			{
				dragonRider[i] = new Character(this, "Dragon_Rider.txt");
			}
			druid = new Character[2];
			for (int i = 0; i < 2; i++)
			{
				druid[i] = new Character(this, "Druid.txt");
			}
			einar = new Character(this, "Einar.txt");
			gauntler = new Character[5];
			for (int i = 0; i < 5; i++)
			{
				gauntler[i] = new Character(this, "Gauntler.txt");
			}
			knight = new Character[2];
			for (int i = 0; i < 2; i++)
			{
				knight[i] = new Character(this, "Knight.txt");
			}
			lion = new Character[2];
			for (int i = 0; i < 2; i++)
			{
				lion[i] = new Character(this, "Lion.txt");
			}
			mage = new Character[2];
			for (int i = 0; i < 2; i++)
			{
				mage[i] = new Character(this, "Mage.txt");
			}
			marksmen = new Character[2];
			for (int i = 0; i < 2; i++)
			{
				marksmen[i] = new Character(this, "Marksman.txt");
			}
			phoenix = new Character(this, "Phoenix.txt");
			shadower = new Character(this, "Shadower.txt");
			soldier = new Character[5];
			for (int i = 0; i < 5; i++)
			{
				soldier[i] = new Character(this, "Soldier.txt");
			}
			valkyrie = new Character[2];
			for (int i = 0; i < 2; i++)
			{
				valkyrie[i] = new Character(this, "Valkyrie.txt");
			}
			wyvern = new Character[2];
			for (int i = 0; i < 2; i++)
			{
				wyvern[i] = new Character(this, "Wyvern.txt");
			}

			mainBoard = new Board(this);
			cursor = new Cursor(this);
			//Place pieces

			//Upholders
			mainBoard.board[0, 8] = valkyrie[0];
			mainBoard.board[0, 7] = mage[0];
			mainBoard.board[0, 6] = lion[0];
			mainBoard.board[0, 5] = blob;
			mainBoard.board[0, 4] = einar;
			mainBoard.board[0, 3] = phoenix;
			mainBoard.board[0, 2] = lion[1];
			mainBoard.board[0, 1] = mage[1];
			mainBoard.board[0, 0] = valkyrie[1];
			mainBoard.board[1, 8] = marksmen[0];
			mainBoard.board[1, 7] = soldier[0];
			mainBoard.board[1, 6] = knight[0];
			mainBoard.board[1, 5] = soldier[1];
			mainBoard.board[1, 4] = soldier[2];
			mainBoard.board[1, 3] = soldier[3];
			mainBoard.board[1, 2] = knight[1];
			mainBoard.board[1, 1] = soldier[4];
			mainBoard.board[1, 0] = marksmen[1];

			//Persecutors
			mainBoard.board[8, 8] = dragonRider[0];
			mainBoard.board[8, 7] = druid[0];
			mainBoard.board[8, 6] = wyvern[0];
			mainBoard.board[8, 5] = blobasaur;
			mainBoard.board[8, 4] = athas;
			mainBoard.board[8, 3] = shadower;
			mainBoard.board[8, 2] = wyvern[1];
			mainBoard.board[8, 1] = druid[1];
			mainBoard.board[8, 0] = dragonRider[1];
			mainBoard.board[7, 8] = callivion[0];
			mainBoard.board[7, 7] = gauntler[0];
			mainBoard.board[7, 6] = assassin[0];
			mainBoard.board[7, 5] = gauntler[1];
			mainBoard.board[7, 4] = gauntler[2];
			mainBoard.board[7, 3] = gauntler[3];
			mainBoard.board[7, 2] = assassin[1];
			mainBoard.board[7, 1] = gauntler[4];
			mainBoard.board[7, 0] = callivion[1];

			//Upholders go first
			ToggleTeam("upholders", cursor, mainBoard);
		}


		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			//Pass spriteBatch into all Draw methods for normal drawing
			spriteBatch.Begin();


			if (!gameOver)
			{
				if (!battleOn)
				{
					//Draw board + cursor if there is no battle
					mainBoard.DrawBoard(spriteBatch);
					cursor.DrawCursor(spriteBatch);
				}
				else
				{
					//Otherwise, draw the battle
					mainBattle.DrawBattle(spriteBatch);
				}
			}
			else
			{
				if (winner == UPHOLDERS)
				{
					spriteBatch.Draw(upholdersWin, viewportRect, Color.White);
				}
				else
				{
					spriteBatch.Draw(persecutorsWin, viewportRect, Color.White);
				}
			}
			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
