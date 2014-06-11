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

		private Boolean[,] GetReachable(int movement, int x, int y, Boolean[,] reach = null)
		{
				
		}	
	}
}
