using Gamee.Framework;
using System.Collections.Generic;

namespace Gamee.Checkers
{
    public class Man : APiece
    {
        public Man(PieceColors color) : base(color, new CheckersPieceTypeAdapter(CheckersPieceType.Man))
        {
        }

        public override List<Coordinate> GetAvailableMoves(Coordinate source, ALayout layout)
        {
            List<Coordinate> availableMoves = new List<Coordinate>();

            int forwardDirection = Color == PieceColors.White ? 1 : -1;

            // Normal moves: one step diagonally forward
            foreach (int offset in new int[] { -1, 1 })
            {
                int newX = source.X + forwardDirection;
                int newY = source.Y + offset;

                if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8 && !layout.ContainsKey(Coordinate.GetInstance(newX, newY)))
                {
                    availableMoves.Add(Coordinate.GetInstance(newX, newY));
                }
            }

            // Capture moves: jump over an opponent's piece
            foreach (int offset in new int[] { -1, 1 })
            {
                int jumpX = source.X + 2 * forwardDirection;
                int jumpY = source.Y + 2 * offset;

                int midX = source.X + forwardDirection;
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

            return availableMoves;
        }
    }
}
