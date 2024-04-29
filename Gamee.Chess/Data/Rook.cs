using Gamee.Framework;
using System.Collections.Generic;

namespace Gamee.Chess
{
    public class Rook : APiece
    {
        public Rook(PieceColors color) : base(color, new ChessPieceTypeAdapter(ChessPieceType.Rook))
        {
        }

        public override List<Coordinate> GetAvailableMoves(Coordinate source, ALayout layout)
        {
            List<Coordinate> availableMoves = new List<Coordinate>();

            // Rook moves horizontally and vertically
            availableMoves.AddRange(GetMovesInDirection(source, 1, 0, layout)); // Vertical (up)
            availableMoves.AddRange(GetMovesInDirection(source, -1, 0, layout)); // Vertical (down)
            availableMoves.AddRange(GetMovesInDirection(source, 0, 1, layout)); // Horizontal (right)
            availableMoves.AddRange(GetMovesInDirection(source, 0, -1, layout)); // Horizontal (left)

            return availableMoves;
        }

        private List<Coordinate> GetMovesInDirection(Coordinate source, int dx, int dy, ALayout layout)
        {
            List<Coordinate> moves = new List<Coordinate>();

            int newX = source.X + dx;
            int newY = source.Y + dy;

            // Keep moving in the specified direction until the edge of the board is reached or an obstacle is encountered
            while (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
            {
                if (!layout.ContainsKey(Coordinate.GetInstance(newX, newY)) ||
                    layout[Coordinate.GetInstance(newX, newY)].Color != this.Color)
                {
                    moves.Add(Coordinate.GetInstance(newX, newY));
                }

                // If there is a piece in the current direction, stop adding moves in that direction
                if (layout.ContainsKey(Coordinate.GetInstance(newX, newY)))
                {
                    break;
                }

                // Check the next square in the direction
                newX += dx;
                newY += dy;
            }

            return moves;
        }
    }
}
