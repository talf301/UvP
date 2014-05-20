using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoMacOriginal
{
	public class Battle
	{
		Character upholderPiece, persecutorPiece;
		Texture2D background;
		Texture2D stop, go, persecutorHP, upholderHP;
		public Cue fightMusic;
		int battleX, battleY, colournew;
		Rock[] rocks;
		Random r;

		int rockCounter;

		public Battle(Game game, Character piece1, Character piece2, int x, int y,int colour)
		{
			colournew = colour;
			battleX = x;
			battleY = y;
			//Assign piece to internal variable based on team
			if (piece1.team == Character.UPHOLDERS)
			{
				upholderPiece = piece1;
				persecutorPiece = piece2;
			}
			else
			{
				upholderPiece = piece2;
				persecutorPiece = piece1;
			}

			if (colour == Board.BOARD_GREEN)
			{
				persecutorPiece.power += 5;
				//fightMusic = Game1.soundBank.GetCue("Persecutors Fight");

				background = game.Content.Load<Texture2D>("Sprites/PersecutorsBattle");
			}
			else if (colour == Board.BOARD_WHITE)
			{
				upholderPiece.power += 5;
				//fightMusic = Game1.soundBank.GetCue("Upholders Fight");
				background = game.Content.Load<Texture2D>("Sprites/UpholdersBattle");
			}
			else if (colour == Board.BOARD_NEUTRAL)
			{
				//fightMusic = Game1.soundBank.GetCue("Neutral Fight");
				background = game.Content.Load<Texture2D>("Sprites/NeutralBattle");
			}
			//Insert if statement once moon phase is implemented


			go = game.Content.Load<Texture2D>("Sprites/GO");
			stop = game.Content.Load<Texture2D>("Sprites/STOP");
			persecutorHP = game.Content.Load<Texture2D>("Sprites/PersecutorsHP");
			upholderHP = game.Content.Load<Texture2D>("Sprites/UpholdersHP");

			//Place pieces on the middle of opposite sides
			upholderPiece.battleX = 30;
			upholderPiece.battleY = 300;
			persecutorPiece.battleX = 720;
			persecutorPiece.battleY = 300;

			//Initialize all of the rocks
			rocks = new Rock[10];
			for (int i = 0; i < 10; i++)
			{
				rocks[i] = new Rock(game);
			}

			//Randomize rocks
			RandomizeRocks();
			//Game1.mainBoard.boardMusic.Pause();
			//fightMusic.Play();
		}

		public void DrawBattle(SpriteBatch spriteBatch)
		{
			//Draw the board itself
			spriteBatch.Draw(background, new Rectangle(0, 0, 800, 600), Color.White);

			//Draw both characters in battle mode
			upholderPiece.DrawBattlecharacter(spriteBatch);
			persecutorPiece.DrawBattlecharacter(spriteBatch);

			//Draw Rocks in loop
			for (int i = 0; i < 10; i++)
			{
				rocks[i].DrawRock(spriteBatch);
			}

			//Draw HP and shoot confirmation
			if (persecutorPiece.ProjectileReady)
			{
				spriteBatch.Draw(go, new Vector2(775, 110), Color.White);
			}
			else
			{
				spriteBatch.Draw(stop, new Vector2(775, 110), Color.White);
			}

			if (upholderPiece.ProjectileReady)
			{
				spriteBatch.Draw(go, new Vector2(5, 110), Color.White);
			}
			else
			{
				spriteBatch.Draw(stop, new Vector2(5, 110), Color.White);
			}

			//Now, draw HP bar by cycling through and drawing the bar as many times as the person has HP
			for (int i = 0; i < persecutorPiece.health; i++)
			{
				spriteBatch.Draw(persecutorHP, new Vector2(775, 550 - i*4), Color.White);
			}

			for( int i = 0; i < upholderPiece.health; i++)
			{
				spriteBatch.Draw(upholderHP, new Vector2(5, 550 - i *4), Color.White);
			}
		}

		public void Update(KeyboardState keyboardState, KeyboardState previousKeyboardState)
		{
			/*Controls for upholder piece*/
			if (keyboardState.IsKeyDown(Keys.W))
			{
				if (CheckCollision(upholderPiece, "up"))
				{
					upholderPiece.battleY -= upholderPiece.speed;
				}
				upholderPiece.RotateCharacter(Character.UP);
			}
			if (keyboardState.IsKeyDown(Keys.S))
			{
				if (CheckCollision(upholderPiece, "down"))
				{
					upholderPiece.battleY += upholderPiece.speed;
				}
				upholderPiece.RotateCharacter(Character.DOWN);
			}
			if (keyboardState.IsKeyDown(Keys.D))
			{
				if (CheckCollision(upholderPiece, "right"))
				{
					upholderPiece.battleX += upholderPiece.speed;
				}
				upholderPiece.RotateCharacter(Character.RIGHT);
			}
			if (keyboardState.IsKeyDown(Keys.A))
			{
				if (CheckCollision(upholderPiece, "left"))
				{
					upholderPiece.battleX -= upholderPiece.speed;
				}
				upholderPiece.RotateCharacter(Character.LEFT);
			}

			//Change directions for upholders if diagonal is used
			if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.D))
			{
				upholderPiece.RotateCharacter(Character.UP_RIGHT);
			}
			if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.A))
			{
				upholderPiece.RotateCharacter(Character.UP_LEFT);
			}
			if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.D))
			{
				upholderPiece.RotateCharacter(Character.DOWN_RIGHT);
			}
			if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.A))
			{
				upholderPiece.RotateCharacter(Character.DOWN_LEFT);
			}

			//Fires for right shift
			if (keyboardState.IsKeyDown(Keys.LeftShift) && !(previousKeyboardState.IsKeyDown(Keys.LeftShift)))
			{
				upholderPiece.Fire();
			}


			/*Controls for persecutors*/
			if (keyboardState.IsKeyDown(Keys.Up))
			{
				if (CheckCollision(persecutorPiece, "up"))
				{
					persecutorPiece.battleY -= persecutorPiece.speed;
				}
				persecutorPiece.RotateCharacter(Character.UP);
			}
			if (keyboardState.IsKeyDown(Keys.Down))
			{
				if (CheckCollision(persecutorPiece, "down"))
				{
					persecutorPiece.battleY += persecutorPiece.speed;
				}
				persecutorPiece.RotateCharacter(Character.DOWN);
			}
			if (keyboardState.IsKeyDown(Keys.Right))
			{
				if (CheckCollision(persecutorPiece, "right"))
				{
					persecutorPiece.battleX += persecutorPiece.speed;
				}
				persecutorPiece.RotateCharacter(Character.RIGHT);
			}
			if (keyboardState.IsKeyDown(Keys.Left))
			{
				if (CheckCollision(persecutorPiece, "left"))
				{
					persecutorPiece.battleX -= persecutorPiece.speed;
				}
				persecutorPiece.RotateCharacter(Character.LEFT);
			}
			//Change direction for persecutors if diagonal is used
			if (keyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyDown(Keys.Right))
			{
				persecutorPiece.RotateCharacter(Character.UP_RIGHT);
			}
			if (keyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyDown(Keys.Left))
			{
				persecutorPiece.RotateCharacter(Character.UP_LEFT);
			}
			if (keyboardState.IsKeyDown(Keys.Down) && keyboardState.IsKeyDown(Keys.Right))
			{
				persecutorPiece.RotateCharacter(Character.DOWN_RIGHT);
			}
			if (keyboardState.IsKeyDown(Keys.Down) && keyboardState.IsKeyDown(Keys.Left))
			{
				persecutorPiece.RotateCharacter(Character.DOWN_LEFT);
			}

			//Fire with right shift
			if (keyboardState.IsKeyDown(Keys.RightShift) && !previousKeyboardState.IsKeyDown(Keys.RightShift))
			{
				persecutorPiece.Fire();
			}

			//Randomize rocks every 10 seconds
			rockCounter ++;
			if (rockCounter == 600)
			{
				RandomizeRocks();
				rockCounter = 0;
			}
			//Updates pieces
			upholderPiece.Update();
			persecutorPiece.Update();

			//Check inter-projectile collision
			if (upholderPiece.Projectile != null && upholderPiece.Projectile.active && persecutorPiece.Projectile != null && persecutorPiece.Projectile.active)
			{
				CheckInterProjCollision(upholderPiece.Projectile, persecutorPiece.Projectile);
			}

			//If the projectiles are active, check the collision
			if (upholderPiece.Projectile != null && upholderPiece.Projectile.active)
			{
				CheckProjCollision(upholderPiece.Projectile, persecutorPiece);
			}
			if (persecutorPiece.Projectile != null && persecutorPiece.Projectile.active)
			{
				CheckProjCollision(persecutorPiece.Projectile, upholderPiece);
			}

			//Check for win
			CheckWin();
		}

		//Method which simply causes attack
		public void Attack(Character receiver)
		{
			if (receiver.team == Character.UPHOLDERS)
			{
				receiver.health -= (persecutorPiece.power - receiver.defense);
			}
			else
			{
				receiver.health -= (upholderPiece.power - receiver.defense);
			}
		}

		//Method which checks if a player has won
		public void CheckWin()
		{
			if (upholderPiece.health <= 0)
			{
				Game1.EndBattle(persecutorPiece, battleX, battleY, colournew);
			}
			else if (persecutorPiece.health <= 0)
			{
				Game1.EndBattle(upholderPiece, battleX, battleY, colournew);
			}
		}
		//Method randomizes all rocks
		public void RandomizeRocks()
		{
			r = new Random();
			//Cycle through rocks and randomize
			for (int i = 0; i < 10; i++)
			{
				rocks[i].x = r.Next(700) + 30;
				rocks[i].y = r.Next(560);
				//Huge if statement just checks if the rock is colliding with a piece, and if it is, randomize again
				if ((!(rocks[i].y + 40 <= upholderPiece.battleY || upholderPiece.battleY + 50 <= rocks[i].y || rocks[i].x + 40 <= upholderPiece.battleX || upholderPiece.battleX + 50 <= rocks[i].x))
					|| (!(rocks[i].y + 40 <= persecutorPiece.battleY || persecutorPiece.battleY + 50 <= rocks[i].y || rocks[i].x + 40 <= persecutorPiece.battleX || persecutorPiece.battleX + 50 <= rocks[i].x)))
				{
					i--;
				}
				//Next, check if it is colliding with any other rocks
				for (int j = 0; j < i; j++)
				{
					if ((!(rocks[i].y + 40 <= rocks[j].y || rocks[j].y + 40 <= rocks[i].y || rocks[i].x + 40 <= rocks[j].x || rocks[j].x + 40 <= rocks[i].x))
						|| (!(rocks[i].y + 40 <= rocks[j].y || rocks[j].y + 50 <= rocks[i].y || rocks[i].x + 40 <= rocks[j].x || rocks[j].x + 50 <= rocks[i].x)))
					{
						i--;
					}
				}
			}
		}

		//Method checks collisions between bullets and things.
		public void CheckProjCollision(Projectile projectile, Character piece)
		{
			int x = projectile.x, y = projectile.y;

			//Ensures it is within boundaries
			if (x >= 30 && x <= 770 - projectile.sprite.Width && y <=600 - projectile.sprite.Height && y >= 0)
			{ 
				//Then cycle through rocks
				for (int i = 0; i < 10; i++)
				{
					if (!(rocks[i].y + 40 <= y || y + projectile.sprite.Height <= rocks[i].y || rocks[i].x + 40 <= x || x + projectile.sprite.Height <= rocks[i].x))
					{
						projectile.Destroy();
						return;
					}
				}

				if (!(piece.battleY + 50 <= y || y + projectile.sprite.Height <= piece.battleY || piece.battleX + 50 <= x || x + projectile.sprite.Height <= piece.battleX))
				{
					Attack(piece);
					projectile.Destroy();
				}
			}
			else
			{
				projectile.Destroy();
			}
		}

		//Method checks collision in between two projectiles, destroys one with less power
		public void CheckInterProjCollision(Projectile uProj, Projectile pProj)
		{
			if (!(uProj.y + uProj.sprite.Height <= pProj.y || pProj.y + pProj.sprite.Height <= uProj.y || uProj.x + uProj.sprite.Width <= pProj.x || pProj.x + pProj.sprite.Width <= uProj.x))
			{
				//If upholder power is better, destroy persecutor piece
				if (upholderPiece.power > persecutorPiece.power)
				{
					pProj.Destroy();
				}
				else if (persecutorPiece.power > upholderPiece.power)
				{
					uProj.Destroy();
				}
				else if (persecutorPiece.power == upholderPiece.power)
				{
					pProj.Destroy();
					uProj.Destroy();
				}
			}
		}


		//Method checks collision detection for rocks + borders
		public Boolean CheckCollision(Character piece, String direction)
		{
			int x, y;

			//Check collision when moving up
			if(direction.Equals("up"))
			{
				//These are co-ordinates if character is moved
				x = piece.battleX;
				y = piece.battleY - piece.speed;

				//First make sure it is on-screen
				if (piece.battleY >= 0)
				{
					//Then cycle through rocks
					for (int i = 0; i < 10; i++)
					{
						if (!(rocks[i].y + 40 <= y || y + 50 <= rocks[i].y || rocks[i].x + 40 <= x || x + 50 <= rocks[i].x))
						{
							return false;
						}                       
					}
				}
				else
				{
					return false;
				}
				return true;
			}
			//Check collision whem moving down
			if (direction.Equals("down"))
			{
				x = piece.battleX;
				y = piece.battleY + piece.speed;
				//First make sure it is on-screen
				if (piece.battleY <= 550)
				{
					//Then cycle through rocks
					for (int i = 0; i < 10; i++)
					{
						if (!(rocks[i].y + 40 <= y || y + 50 <= rocks[i].y || rocks[i].x + 40 <= x || x + 50 <= rocks[i].x))
						{
							return false;
						} 
					}
				}
				else
				{
					return false;
				}
				return true;
			}
			//Check collision when moving right
			if (direction.Equals("right"))
			{
				x = piece.battleX + piece.speed;
				y = piece.battleY;
				//First make sure it is on-screen
				if (piece.battleX <= 720)
				{
					//Then cycle through rocks
					for (int i = 0; i < 10; i++)
					{
						if (!(rocks[i].y + 40 <= y || y + 50 <= rocks[i].y || rocks[i].x + 40 <= x || x + 50 <= rocks[i].x))
						{
							return false;
						} 
					}
				}
				else
				{
					return false;
				}
				return true;
			}
			//Checking collision when moving left
			if (direction.Equals("left"))
			{
				x = piece.battleX - piece.speed;
				y = piece.battleY;
				//First make sure it is on-screen
				if (piece.battleX >= 30)
				{
					//Then cycle through rocks
					for (int i = 0; i < 10; i++)
					{
						if (!(rocks[i].y + 40 <= y || y + 50 <= rocks[i].y || rocks[i].x + 40 <= x || x + 50 <= rocks[i].x))
						{
							return false;
						} 
					}
				}
				else
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
