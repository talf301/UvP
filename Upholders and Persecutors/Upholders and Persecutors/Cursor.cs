using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMacOriginal
{
    public class Cursor
    {
        //Constants and int for person using
        const int UPHOLDERS = 0, PERSECUTORS = 1;
        int user;
        //Holds both sprite for light and for dark
        Texture2D[] sprite;
        //Holds faded sprite for light and dark
        Texture2D[] fadedSprite;
        //Array holds all possible moves as booleans
        Boolean[,] availableMoves;
        //Array holds all selectable squares
        public Boolean[,] selectableSquares;
        public int x, y;
        int selectedX, selectedY;
        //Boolean stores wether or not a piece has been selected
        public Boolean selected = false;

        //Constructor sets all moves as available and assigns images
        public Cursor(Game game)
        {
            sprite = new Texture2D[2];
            fadedSprite = new Texture2D[2];
            user = UPHOLDERS;
            availableMoves = new Boolean[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    availableMoves[i, j] = true;
                }
            }

            sprite[UPHOLDERS] = game.Content.Load<Texture2D>("Sprites\\UpholdersCursor");
            sprite[PERSECUTORS] = game.Content.Load<Texture2D>("Sprites\\PersecutorsCursor");
            fadedSprite[UPHOLDERS] = game.Content.Load<Texture2D>("Sprites\\UpholdersCursorSelect");
            fadedSprite[PERSECUTORS] = game.Content.Load<Texture2D>("Sprites\\PersecutorsCursorSelect");

        }
        //Method simply tells cursor a piece has been selected
        public void Select()
        {
            selected = true;
            selectedX = x;
            selectedY = y;
        }

        public void Unselect()
        {
            selected = false;
            availableMoves = new Boolean[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    availableMoves[i, j] = true;
                }
            }

        }

        //Method to change available moves
        public void SetAvailableMoves(Boolean[,] bools)
        {
            availableMoves = bools;
        }

        //Method to change selectable squares
        public void SetSelectableSquares(Boolean[,] bools)
        {
            selectableSquares = bools;
        }

        //Method to move cursor
        public void Move(String direction)
        {
            
            if(direction.Equals("up"))
            {
                //Only if the movement would be onscreen
                if (y < 9)
                {
                    //If the spot trying to be moved to is legal, move ( there is an offset of -1 due to array indexing)
                    if (availableMoves[x - 1, y])
                    {
                        y++;
                    }
                }
            }
            else if (direction.Equals("down"))
            {
                if (y > 1)
                {
                    if (availableMoves[x - 1, y - 2])
                    {
                        y--;
                    }
                }
            }
            else if (direction.Equals("right"))
            {
                if (x < 9)
                {
                    if (availableMoves[x, y - 1])
                    {
                        x++;
                    }
                }
            }
            else if (direction.Equals("left"))
            {
                if (x > 1)
                {
                    if (availableMoves[x - 2, y - 1])
                    {
                        x--;
                    }
                }
            }
        }

        //Method to set cursor back to starting point based on side (taken as string argument)
        public void Reset(String side)
        {
            if (side.Equals("upholders"))
            {
                x = 1;
                y = 5;
            }
            else if (side.Equals("persecutors"))
            {
                x = 9;
                y = 5;
            }
        }

        //Method to draw cursor with offset
        public void DrawCursor(SpriteBatch spriteBatch)
        {
            //If a piece is selected draw the faint cursor
            if (selected)
            {
                spriteBatch.Draw(fadedSprite[user], new Rectangle(125 + (selectedX - 1) * 60, (9 - selectedY) * 60, fadedSprite[user].Width, fadedSprite[user].Height), Color.White);
            }
            spriteBatch.Draw(sprite[user], new Rectangle(125 + (x - 1) * 60, (9 - y) * 60, sprite[user].Width, sprite[user].Height), Color.White);
        }
        //Method to toggle sprite
        public void ToggleUser(String team)
        {
            if(team.Equals("upholders"))
            {
                user = UPHOLDERS;
            }
            else if (team.Equals("persecutors"))
            {
                user = PERSECUTORS;
            }
        }//Toggle Method
    }//Class
}//Namespace
