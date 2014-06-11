using Artemis;
using Artemis.Interface;

namespace UvpRework
{
	public class CursorInfo : IComponent
	{
		private Entity[,] Board;
		private int x,y, SelX, SelY;
		private Boolean Selected;
		private Boolean[,] Selectable;
		private Boolean[,] Moveable;
		private Team team;
		public CursorInfo()
		{
			Board = BoardState.GetInstance().GetState();
			team = Team.UPHOLDERS;
			Selectable = new Boolean[9,9];
			UpdateSelectable();
			Moveable = new Boolean[9,9];
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
		
		//Assume a square is selected
		public void UpdateSelect()
		{
			Entity e = Board[SelX, SelY];
			if(e.GetComponent<BoardInfo>().IsFlying())
			{
				for(int i = 0; i < Board.GetLength(0); i++)
				{
					for(int j = 0; j < Board.GetLength(1); j++)
					{
						Moveable[i,j] = Math.Max(Math.Abs(i-SelX), Math.Abs(j-SelY)) <= e.GetComponent<BoardInfo>().GetMovement();
						Selectable[i,j] = Moveable[i,j] && (Board[i,j] == null || Board[i,j] == e || Board[i,j].GetComponent<BoardInfo>().GetTeam() != e.GetComponent<BoardInfo>().GetTeam());
					}
				}
			}
			else
			{
				
			}
		}

		private Boolean[,] GetReachable(int movement, int x, int y,	Team team, Boolean[,] reach = null)
		{
			if(movement == 0)
				return reach;
			if(reach == null)
			{
				reach = new Boolean[9,9]();
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
							GetReachable(movement-1,x+i,y+j,reach);
						}

						if(!reach[x+i,y+j] && team != Board[x+i,y+j].GetComponent<BoardInfo>().GetTeam())
						{
							reach[x+i,y+j] = true;
						}
					}
				}
			}
		}	
	}
}
