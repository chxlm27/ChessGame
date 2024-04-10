// King.cs
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
    public class King : APiece
    {
        public King(PieceColors color) : base(color, PieceType.King)
        {
        }

        public override List<Coordinate> GetAvailableMoves(Coordinate source) // context de joc
        {
            List<Coordinate> availableMoves = new List<Coordinate>();

            // King moves one step in any direction (including diagonals)
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    // Skip the position where the piece is currently located
                    if (dx == 0 && dy == 0)
                        continue;

                    int newX = source.X + dx;
                    int newY = source.Y + dy;

                    // Check if the new position is within the bounds of the board
                    if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                    {
                        availableMoves.Add(Coordinate.GetInstance(newX, newY));
                    }
                }
            }

            return availableMoves;
        }

    }
}
