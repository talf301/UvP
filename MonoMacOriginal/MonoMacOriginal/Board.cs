using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace MonoMacOriginal
{
	public class Board
	{
		//State of board
		public const int BOARD_NEUTRAL = 0, BOARD_GREEN = 1, BOARD_WHITE = 2;
		public int[,] boardColours;
		TextReader tr;
		Game mainGame;
		public Cue boardMusic;
		Texture2D[] boards;
		Texture2D background;
		Character temp;
		//Array of characters responsible for board state
		public Character[,] board;

		public Board(Game game)
		{
			mainGame = game;
			//Read in positions of different colours from text file
			tr = new StreamReader("Content/Data/BoardNeutral.txt");
			boardColours = new int[9,9];
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					boardColours[j, i] = int.Parse( tr.ReadLine());
				}
			}
			boards = new Texture2D[3];
			boards[BOARD_NEUTRAL] = game.Content.Load<Texture2D>("Sprites/BoardNeutral");
			boards[BOARD_GREEN] = game.Content.Load<Texture2D>("Sprites/BoardGreen");
			boards[BOARD_WHITE] = game.Content.Load<Texture2D>("Sprites/BoardWhite");
			background = game.Content.Load<Texture2D>("Sprites/BoardBackground");
			board = new Character[9, 9];
			//boardMusic = Game1.soundBank.GetCue("BoardMusic");
			//boardMusic.Play();
		}

		//Draw method draws board and contained pieces
		public void DrawBoard(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
			spriteBatch.Draw(boards[0], new Rectangle(130, 5, 540, 540), Color.White);

			//2 for loops to cycle through all elements and draw them
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					if(board[i,j] != null)
					{
						board[i, j].DrawBoardCharacter(spriteBatch, i, j);
					}
				}
			}
		}

		//Method which returns the piece at a certain co-ordinate (used in conjuction with cursor)
		public Character CheckPiece(int x, int y)
		{
			return board[x - 1, y - 1];

		}

		//Method which returns 2D array of booleans representing all legal moves
		public Boolean[,] LegalMoves(Character checker)
		{
			int x = checker._x;
			int y = checker._y;
			Boolean[,] returned = new Boolean[9,9];
			//For loop goes through all spaces and checks if the move is within reach, and if there is a piece of the same color there

			//First the option for ranged pieces
			if (checker.flying)
			{
				for (int i = 0; i < 9; i++)
				{
					for (int j = 0; j < 9; j++)
					{
						//Check if spot is reachable
						if (Math.Abs(i - x) <= checker.movement && Math.Abs(j - y) <= checker.movement)
						{
							//Only check spot if there is something there
							if (board[i, j] != null)
							{
								//Check is spot is occupied by same team, the "or"ensures that one can move back on to original piece
								if (board[i, j].team != checker.team || (x == i && y == j))
								{
									returned[i, j] = true;
									Game1.cursor.selectableSquares[i, j] = true;
								}
								else
									//If the it is the same person, the place is movable but not selectable.
								{
									returned[i, j] = true;
									Game1.cursor.selectableSquares[i, j] = false;
								}
							}
							else
							{
								returned[i, j] = true;
								Game1.cursor.selectableSquares[i, j] = true;
							}
						}
						else
						{
							returned[i, j] = false;
						}
					}
				}
			}

			if (!checker.flying)
			{
				for (int i = 0; i < 9; i++)
				{
					for (int j = 0; j < 9; j++)
					{
						//Check if spot is reachable
						if (Math.Abs(i - x) + Math.Abs(j - y) <= checker.movement)

						{
							//Only check spot if there is something there
							if (board[i, j] != null)
							{
								//Check is spot is occupied by same team, the and ensures that one can move back on to original piece
								if (board[i, j].team != checker.team || (x == i && y == j))
								{
									returned[i, j] = true;
									Game1.cursor.selectableSquares[i, j] = true;
								}
								else
								{
									returned[i, j] = false;
									Game1.cursor.selectableSquares[i, j] = false;
								}
							}
							else
							{
								returned[i, j] = true;
								Game1.cursor.selectableSquares[i, j] = true;
							}
						}
						else
						{
							returned[i, j] = false;
							Game1.cursor.selectableSquares[i, j] = false;
						}
					}
				}
			}
			return returned;
		}
		//Method which returns 2D array of booleans representing all selectable squares
		public Boolean[,] SelectableSquares(String team)
		{
			Boolean[,] returned = new Boolean[9, 9];
			if (team.Equals("upholders"))
			{
				for (int i = 0; i < 9; i++)
				{
					for (int j = 0; j < 9; j++)
					{
						if (board[i, j] != null)
						{
							if (board[i, j].team == Character.UPHOLDERS)
							{
								returned[i, j] = true;
							}
							else
							{
								returned[i, j] = false;
							}
						}
						else
						{
							returned[i, j] = false;
						}
					}
				}
			}

			else if (team.Equals("persecutors"))
			{
				for (int i = 0; i < 9; i++)
				{
					for (int j = 0; j < 9; j++)
					{
						if (board[i, j] != null)
						{

							if (board[i, j].team == Character.PERSECUTORS)
							{
								returned[i, j] = true;
							}
							else
							{
								returned[i, j] = false;
							}
						}
						else
						{
							returned[i, j] = false;
						}
					}
				}
			}
			Console.WriteLine("boo");
			return returned;
		}

		//Method which moves given piece to given spot
		public void MovePiece(Character piece, int x, int y, Game1 game)
		{

			board[piece._x, piece._y] = null;
			if (board[x - 1, y - 1] == null)
			{
				board[x - 1, y - 1] = piece;
			}
			else
			{
				game.TriggerBattle(piece, board[x - 1, y - 1], x - 1, y - 1);
			}
		}

		public void CaptureSpace(Character piece, int x, int y)
		{        
			board[x, y] = piece;
		}

		//Method which updates using keys
		public void Update(KeyboardState keyboardState, KeyboardState previousKeyboardState, Cursor cursor, Board board, Game1 game)
		{
			/*Movement options*/


			/*First segment is for upholders only*/
			if (Game1.activeTeam == Character.UPHOLDERS)
			{
				if (keyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W))
				{
					cursor.Move("up");
				}

				if (keyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S))
				{
					cursor.Move("down");
				}

				if (keyboardState.IsKeyDown(Keys.D) && !previousKeyboardState.IsKeyDown(Keys.D))
				{
					cursor.Move("right");
				}

				if (keyboardState.IsKeyDown(Keys.A) && !previousKeyboardState.IsKeyDown(Keys.A))
				{
					cursor.Move("left");
				}

				//Shift selects
				if (keyboardState.IsKeyDown(Keys.LeftShift) && !previousKeyboardState.IsKeyDown(Keys.LeftShift))
				{
					//Only let user pick move if cursor is not selected and square is selectable
					if (!cursor.selected && cursor.selectableSquares[cursor.x - 1, cursor.y - 1])
					{

						temp = board.CheckPiece(cursor.x, cursor.y);
						cursor.SetAvailableMoves(board.LegalMoves(temp));
						cursor.Select();
					}
					else if (cursor.selected)
					{
						//If they reselect the same square, then simply unselect
						if ((!(cursor.x == temp._x + 1 && cursor.y == temp._y + 1)) && cursor.selectableSquares[cursor.x - 1, cursor.y - 1])
						{
							board.MovePiece(temp, cursor.x, cursor.y, game);

							//After move has been made, toggle the active team
							Game1.ToggleTeam("persecutors", cursor, board);
							cursor.Unselect();
						}
						else if (cursor.x == temp._x + 1 && cursor.y == temp._y + 1)
						{
							cursor.Unselect();
							cursor.SetSelectableSquares(board.SelectableSquares("upholders"));
						}
					}

				}
			}

			/*Second segment is for persecutors only*/
			else
			{
				//Only do movement if the person has just pressed up
				if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up))
				{
					cursor.Move("up");
				}

				if (keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))
				{
					cursor.Move("down");
				}

				if (keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))
				{
					cursor.Move("right");
				}

				if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left))
				{
					cursor.Move("left");
				}

				//Shift selects
				if (keyboardState.IsKeyDown(Keys.RightShift) && !previousKeyboardState.IsKeyDown(Keys.RightShift))
				{
					//Only let user pick move if cursor is not selected and square is selectable
					if (!cursor.selected && cursor.selectableSquares[cursor.x - 1, cursor.y - 1])
					{

						temp = board.CheckPiece(cursor.x, cursor.y);
						cursor.SetAvailableMoves(board.LegalMoves(temp));
						cursor.Select();
					}
					else if (cursor.selected)
					{  
						//If they reselect the same square, then simply unselect
						if ((!(cursor.x == temp._x + 1 && cursor.y == temp._y + 1)) && cursor.selectableSquares[cursor.x - 1, cursor.y - 1])
						{
							board.MovePiece(temp, cursor.x, cursor.y, game);

							//After move has been made, toggle the active team
							Game1.ToggleTeam("upholders", cursor, board);
							cursor.Unselect();
						}
						else if (cursor.x == temp._x + 1 && cursor.y == temp._y + 1)
						{
							cursor.Unselect();
							cursor.SetSelectableSquares(board.SelectableSquares("persecutors"));
						}
					}

				}
			}
		}
	}
}
