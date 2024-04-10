using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
    public class Rook : APiece
    {
        public Rook(PieceColors color) : base(color, PieceType.Rook)
        {
        }

        public override List<Coordinate> GetAvailableMoves(Coordinate source)
        {
            List<Coordinate> availableMoves = new List<Coordinate>();

            // Rook moves horizontally and vertically
            availableMoves.AddRange(GetMovesInDirection(source, 1, 0)); // Vertical (up)
            availableMoves.AddRange(GetMovesInDirection(source, -1, 0)); // Vertical (down)
            availableMoves.AddRange(GetMovesInDirection(source, 0, 1)); // Horizontal (right)
            availableMoves.AddRange(GetMovesInDirection(source, 0, -1)); // Horizontal (left)

            return availableMoves;
        }

        private List<Coordinate> GetMovesInDirection(Coordinate source, int dx, int dy)
        {
            List<Coordinate> moves = new List<Coordinate>();

            int newX = source.X + dx;
            int newY = source.Y + dy;

            // Keep moving in the specified direction until the edge of the board is reached
            while (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
            {
                moves.Add(Coordinate.GetInstance(newX, newY));

                // Check the next square in the direction
                newX += dx;
                newY += dy;
            }

            return moves;
        }

    }
}
