using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
    public class Knight : APiece
    {
        public Knight(PieceColors color) : base(color, PieceType.Knight)
        {
        }

        public override List<Coordinate> GetAvailableMoves(Coordinate source)
        {
            List<Coordinate> availableMoves = new List<Coordinate>();

            // Knight moves in an "L" shape pattern
            int[] dx = { -2, -1, 1, 2, 2, 1, -1, -2 };
            int[] dy = { 1, 2, 2, 1, -1, -2, -2, -1 };

            for (int i = 0; i < dx.Length; i++)
            {
                int newX = source.X + dx[i];
                int newY = source.Y + dy[i];

                // Check if the new position is within the bounds of the board
                if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                {
                    availableMoves.Add(Coordinate.GetInstance(newX, newY));
                }
            }

            return availableMoves;
        }

    }
}
