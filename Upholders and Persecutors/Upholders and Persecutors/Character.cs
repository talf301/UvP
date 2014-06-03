using System;
using System.IO;
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


namespace Upholders_vs_Persecutors
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Character : Microsoft.Xna.Framework.GameComponent
    {
        /*Constants for directions*/
        public const int DOWN = 0, DOWN_LEFT = 1, LEFT = 2, UP_LEFT = 3, UP = 4, UP_RIGHT = 5, RIGHT = 6, DOWN_RIGHT = 7;
        public const int ATT_DOWN = 8, ATT_DOWN_LEFT = 9, ATT_LEFT = 10, ATT_UP_LEFT = 11, ATT_UP = 12, ATT_UP_RIGHT = 13, ATT_RIGHT = 14, ATT_DOWN_RIGHT = 15;
        public const Boolean UPHOLDERS = true, PERSECUTORS = false;
        #region Variable Declarations
        int rechargeSpeed;
        int counter;

        private bool projectileReady = true;
        public bool ProjectileReady
        {
            get { return projectileReady; }
            set { projectileReady = value; }
        }
        
        private Texture2D[] sprites;
        private Texture2D proj;

        public Texture2D Proj
        {
            get { return proj; }
            set { proj = value; }
        }

        private Projectile projectile = null;
        public Projectile Projectile
        {
            get { return projectile; }
        }

        TextReader tr;
        public int movement, power, speed, health, defense, projSpeed;
        public bool ranged, flying, team;
        public int direction = DOWN;
        public int _x, _y, battleX, battleY;
        String spriteLocation;
        #endregion

        public Character(Game game, String filePath)
            : base(game)
        {
            FileToChar(filePath);
            counter = rechargeSpeed;
            Initialize(game.Content);
        }

        public void FileToChar(String fileName)
        {
            tr = new StreamReader("Content\\Data\\Characters\\" + fileName);
            movement = int.Parse(tr.ReadLine());
            power = int.Parse(tr.ReadLine());
            speed = int.Parse(tr.ReadLine());
            health = int.Parse(tr.ReadLine());
            defense = int.Parse(tr.ReadLine());
            projSpeed = int.Parse(tr.ReadLine());
            rechargeSpeed = int.Parse(tr.ReadLine());
            ranged = parseBool(tr.ReadLine());
            flying = parseBool(tr.ReadLine());
            team = parseBool(tr.ReadLine());
            spriteLocation = tr.ReadLine();
        }
        public bool parseBool(String input)
        {
            if (input.Equals("True") || input.Equals("true"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        
        //Initialization method assigns sprite to array of different sized based on "ranged" variable
        public void Initialize(ContentManager content)
        {
            if (!ranged)
            {
                sprites = new Texture2D[16];
                for (int i = 0; i < 16; i++)
                {
                    sprites[i] = content.Load<Texture2D>(spriteLocation + "Frame" + (i));
                }
            }
            else
            {
                sprites = new Texture2D[8];
                for (int i = 0; i < 8; i++)
                {
                    sprites[i] = content.Load<Texture2D>(spriteLocation + "Frame" + (i));
                }
                proj = content.Load<Texture2D>(spriteLocation + "Projectile");
            }

            
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update()
        {
            //Only move the projectile if it exists
            if (Projectile != null && Projectile.active)
            {
                Projectile.Update();
            }
            if (counter < rechargeSpeed)
            {
                counter++;
            }
            else if (counter == rechargeSpeed)
            {
                ProjectileReady = true;
            }
        }

        //Method fires the projectile by creating a new projectile
        public void Fire()
        {
            if (ProjectileReady)
            {
                if (ranged)
                {
                    //Create new projectile
                    projectile = new Projectile(this);
                    //Start counter again
                    counter = 0;
                    ProjectileReady = false;
                    //Play sound effect :D
                    Game1.soundBank.PlayCue("RangedAttack");
                }
            }

        }

        //Method to rotate character
        public void RotateCharacter(int newDirection)
        {
            direction = newDirection;
        }
        
        //Method to draw characters while on board; includes x and y offset
        public void DrawBoardCharacter(SpriteBatch spriteBatch, int x, int y)
        {
            if (team == UPHOLDERS)
            {
                spriteBatch.Draw(sprites[6], new Rectangle(135 + (60 * x), (8 - y) * 60 + 10, sprites[6].Width, sprites[6].Height), Color.White);
            }
            else if(team == PERSECUTORS)
            {
                spriteBatch.Draw(sprites[2], new Rectangle(135 + (60 * x), (8 - y) * 60 + 10, sprites[2].Width, sprites[2].Height), Color.White);
            }
            _x = x;
            _y = y;
        }

        //Method to draw characters during battle
        public void DrawBattlecharacter(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprites[direction], new Rectangle(battleX, battleY, sprites[direction].Width, sprites[direction].Height), Color.White);
            //Again, only draw the projectile if it exists
            if (Projectile != null && Projectile.active)
            {
                Projectile.DrawProjectile(spriteBatch);
            }
        }
    }
}
