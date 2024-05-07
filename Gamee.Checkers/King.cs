using Gamee.Framework;
using System.Collections.Generic;

namespace Gamee.Checkers
{
    public class King : APiece
    {
        public King(PieceColors color) : base(color, new CheckersPieceTypeAdapter(CheckersPieceType.King))
        {
        }

        public override List<Coordinate> GetAvailableMoves(Coordinate source, ALayout layout)
        {
            List<Coordinate> availableMoves = new List<Coordinate>();

            // Kings move both forward and backward
            int[] directions = new int[] { 1, -1 };

            foreach (int direction in directions)
            {
                // Normal moves: one step diagonally in each direction
                foreach (int offset in new int[] { -1, 1 })
                {
                    int newX = source.X + direction;
                    int newY = source.Y + offset;

                    if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8 && !layout.ContainsKey(Coordinate.GetInstance(newX, newY)))
                    {
                        availableMoves.Add(Coordinate.GetInstance(newX, newY));
                    }
                }

                // Capture moves: jump over an opponent's piece
                foreach (int offset in new int[] { -1, 1 })
                {
                    int jumpX = source.X + 2 * direction;
                    int jumpY = source.Y + 2 * offset;

                    int midX = source.X + direction;
                    int midY = source.Y + offset;

                    if (jumpX >= 0 && jumpX < 8 && jumpY >= 0 && jumpY < 8)
                    {
                        Coordinate midPosition = Coordinate.GetInstance(midX, midY);
                        Coordinate jumpPosition = Coordinate.GetInstance(jumpX, jumpY);

                        // Check if the middle spot has an opponent's piece and the jump position is empty
                        if (layout.ContainsKey(midPosition) && layout[midPosition].Color != Color && !layout.ContainsKey(jumpPosition))
                        {
                            availableMoves.Add(jumpPosition);
                        }
                    }
                }
            }

            return availableMoves;
        }
    }
}
