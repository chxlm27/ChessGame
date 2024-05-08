using Gamee.Framework;
using System.Collections.Generic;

namespace Gamee.Chess
{
    public class King : APiece
    {
        public King(PieceColors color) : base(color, new ChessPieceTypeAdapter(ChessPieceType.King))
        {
        }

        public override List<Coordinate> GetAvailableMoves(Coordinate source, ALayout layout)
        {
            List<Coordinate> availableMoves = new List<Coordinate>();

            // King moves one step in any direction (including diagonals)
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                int newX = source.X + dx[i];
                int newY = source.Y + dy[i];

                // Check if the new position is within the bounds of the board
                if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                {
                    // Check if the position is empty or occupied by an opponent's piece
                    if (!layout.ContainsKey(Coordinate.GetInstance(newX, newY)) ||
                        layout[Coordinate.GetInstance(newX, newY)].Color != Color)
                    {
                        availableMoves.Add(Coordinate.GetInstance(newX, newY));
                    }
                }
            }

            return availableMoves;
        }

    }
}
