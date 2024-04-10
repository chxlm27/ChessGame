using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
    public class Queen : APiece
    {
        public Queen(PieceColors color) : base(color, PieceType.Queen)
        {
        }

        public override List<Coordinate> GetAvailableMoves(Coordinate source)
        {
            List<Coordinate> availableMoves = new List<Coordinate>();

            // Queen moves diagonally, horizontally, and vertically
            availableMoves.AddRange(GetMovesInDirection(source, 1, 0)); // Vertical
            availableMoves.AddRange(GetMovesInDirection(source, 0, 1)); // Horizontal
            availableMoves.AddRange(GetMovesInDirection(source, 1, 1)); // Diagonal (top-right)
            availableMoves.AddRange(GetMovesInDirection(source, 1, -1)); // Diagonal (top-left)
            availableMoves.AddRange(GetMovesInDirection(source, -1, 1)); // Diagonal (bottom-right)
            availableMoves.AddRange(GetMovesInDirection(source, -1, -1)); // Diagonal (bottom-left)

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
