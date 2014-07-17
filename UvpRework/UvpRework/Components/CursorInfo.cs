using System;
using Artemis;
using Artemis.Interface;

namespace UvpRework
{
	public class CursorInfo : IComponent
	{
		private Entity[,] Board;
		private int x;
		public int X {get; private set;}
		private int y;
	    public int Y {get; private set;}	
		private int	SelX, SelY;
		private bool Selected;
		private bool[,] Selectable;
		private bool[,] Moveable;
		private Team team;
		public CursorInfo()
		{
			Board = BoardState.GetInstance().GetState();
			this.Reset(Team.UPHOLDERS);
			Selected = false;
			Selectable = new bool[9,9];
			UpdateNonSelect();
			Moveable = new bool[9,9];
		}

		private void UpdateNonSelect()
		{
			for(int i = 0; i < Board.GetLength(0); i++)
			{
				for(int j = 0; j < Board.GetLength(1); j++)
				{
					Selectable[i,j] = Board[i,j] != null && Board[i,j].GetComponent<BoardInfo>().GetTeam() == team;
				}
			}	
		}
	
		public void Reset(Team team)
		{
			if(team == Team.UPHOLDERS)
			{
				x = 1;
				y = 5;
			}
			else
			{
				x = 9;
				y = 5;
			}
			this.team = team;
		}


		//Assume a square is selected
		public void UpdateSelect()
		{
			Entity e = Board[SelX, SelY];
			BoardInfo info = e.GetComponent<BoardInfo>();
			if(info.IsFlying())
			{
				for(int i = 0; i < Board.GetLength(0); i++)
				{
					for(int j = 0; j < Board.GetLength(1); j++)
					{
						Moveable[i,j] = Math.Max(Math.Abs(i - SelX), Math.Abs(j - SelY)) <= info.GetMovement();
						Selectable[i,j] = Moveable[i,j] && (Board[i,j] == null || Board[i,j] == e || Board[i,j].GetComponent<BoardInfo>().GetTeam() != info.GetTeam());
					}
				}
			}
			else
			{
				Moveable = GetReachable(e.GetComponent<BoardInfo>().GetMovement(),SelX, SelY, info.GetTeam());
				Selectable = (bool[,])Moveable.Clone();	
			}
		}

		private bool[,] GetReachable(int movement, int x, int y, Team team, bool[,] reach = null)
		{
			if(movement == 0)
				return reach;
			if(reach == null)
			{
				reach = new bool[9,9];
				reach[x,y] = true;
			}
			for(int i = -1; i < 2; i++)
			{
				for(int j = -1; j < 2; j++)
				{
					if(i*i + j*j == 1)
					{
						if(!reach[x+i,y+j] && Board[x+i,y+j] == null)
						{
							reach[x+i,y+j] = true;
							GetReachable(movement-1,x+i,y+j,team,reach);
						}

						if(!reach[x+i,y+j] && team != Board[x+i,y+j].GetComponent<BoardInfo>().GetTeam())
						{
							reach[x+i,y+j] = true;
						}
					}
				}
			}
			return reach;

		}	
	}
}
