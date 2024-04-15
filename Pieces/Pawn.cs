using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
    public class Pawn : APiece
    {
        public Pawn(PieceColors color) : base(color, PieceType.Pawn)
        {
        }

        public override List<Coordinate> GetAvailableMoves(Coordinate source, ALayout layout)
        {
            List<Coordinate> availableMoves = new List<Coordinate>();

            // Pawn moves forward one square (opposite direction)
            int forwardDirection = (Color == PieceColors.White) ? 1 : -1;
            int newX = source.X + forwardDirection;

            // Check if the new position is within the bounds of the board and if the square in front is empty
            if (newX >= 0 && newX < 8)
            {
                // Check if the square in front is empty
                if (!layout.ContainsKey(Coordinate.GetInstance(newX, source.Y)))
                {
                    availableMoves.Add(Coordinate.GetInstance(newX, source.Y));

                    // Add initial double move for pawn if the square two squares ahead is also empty
                    if ((Color == PieceColors.White && source.X == 1) || (Color == PieceColors.Black && source.X == 6))
                    {
                        int doubleMoveX = source.X + forwardDirection * 2;
                        // Check if the squares in front of the pawn are empty for both single and double moves
                        if (doubleMoveX >= 0 && doubleMoveX < 8 && !layout.ContainsKey(Coordinate.GetInstance(doubleMoveX, source.Y)))
                        {
                            availableMoves.Add(Coordinate.GetInstance(doubleMoveX, source.Y));
                        }
                    }
                }
            }

            // Add capturing moves diagonally
            int[] captureOffsets = { -1, 1 };
            foreach (int offset in captureOffsets)
            {
                int captureX = source.X + forwardDirection;
                int captureY = source.Y + offset;

                if (captureX >= 0 && captureX < 8 && captureY >= 0 && captureY < 8)
                {
                    Coordinate capturePosition = Coordinate.GetInstance(captureX, captureY);
                    // Check if there's an opponent's piece to capture
                    if (layout.ContainsKey(capturePosition) && layout[capturePosition].Color != Color)
                    {
                        availableMoves.Add(capturePosition);
                    }
                }
            }

            return availableMoves;
        }
    }
}
