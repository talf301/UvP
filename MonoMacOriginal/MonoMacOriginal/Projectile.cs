using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMacOriginal
{
	public class Projectile
	{
		public Texture2D sprite;
		public int x, y, xInc, yInc;
		public Boolean active;
		public Projectile(Character piece)
		{
			sprite = piece.Proj;
			active = true;
			//Set initial location and frame incrementation based on original piece.direction and piece.projSpeed
			switch (piece.direction)
			{
			case Character.DOWN:
				x = piece.battleX + (50 - piece.Proj.Width) / 2;
				y = piece.battleY + 50;
				xInc = 0;
				yInc = piece.projSpeed;
				break;

			case Character.DOWN_LEFT:
				x = piece.battleX - piece.Proj.Width;
				y = piece.battleY + 50;
				xInc = -piece.projSpeed;
				yInc = piece.projSpeed;
				break;

			case Character.LEFT:
				x = piece.battleX - piece.Proj.Width;
				y = piece.battleY + (50 - piece.Proj.Height) / 2;
				xInc = -piece.projSpeed;
				yInc = 0;
				break;

			case Character.UP_LEFT:
				x = piece.battleX - piece.Proj.Width;
				y = piece.battleY - piece.Proj.Height;
				xInc = -piece.projSpeed;
				yInc = -piece.projSpeed;
				break;

			case Character.UP:
				x = piece.battleX + (50 - piece.Proj.Width) / 2;
				y = piece.battleY - piece.Proj.Height;
				xInc = 0;
				yInc = -piece.projSpeed;
				break;

			case Character.UP_RIGHT:
				x = piece.battleX + 50;
				y = piece.battleY - piece.Proj.Height;
				xInc = piece.projSpeed;
				yInc = -piece.projSpeed;
				break;

			case Character.RIGHT:
				x = piece.battleX + 50;
				y = piece.battleY + (50 - piece.Proj.Width) / 2;
				xInc = piece.projSpeed;
				yInc = 0;
				break;

			case Character.DOWN_RIGHT:
				x = piece.battleX + 50;
				y = piece.battleY + 50;
				xInc = piece.projSpeed;
				yInc = piece.projSpeed;
				break;
			}
		}

		public void Update()
		{
			//Move projectile by incrementation
			x += xInc;
			y += yInc;
		}

		public void DrawProjectile(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(sprite, new Vector2(x, y), Color.White);
		}

		public void Destroy()
		{
			active = false;
		}
	}
}
