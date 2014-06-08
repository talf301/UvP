using Artemis;
using Artemis.Interface;

namespace UvpRework
{
	public class CursorInfo : IComponent
	{
		private Entity[,] Board;
		private int x,y;
		private Boolean[,] Selectable;
		private Boolean[,] Moveable;
		private Team team;
		public CursorInfo()
		{
			Board = BoardState.GetInstance().GetState();
			team = Team.UPHOLDERS;
			Selectable = new Boolean[8,8];
		}

		private void UpdateSelectable()
		{
			for(int i = 0; i < Board.GetLength(0); i++)
			{
				for(int j = 0; j < Board.GetLength(1); j++)
				{
					Selectable[i,j] = Board[i,j] != null && Board[i,j].GetComponent<BoardInfo>().GetTeam() == team;
				}
			}	
		}	
	}
}
