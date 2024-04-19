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

            int forwardDirection = (Color == PieceColors.White) ? 1 : -1;
            int newX = source.X + forwardDirection;

            // Check if the new position is within the bounds of the board and if the square in front is empty
            if (newX >= 0 && newX < 8 && !layout.ContainsKey(Coordinate.GetInstance(newX, source.Y)))
            {
                availableMoves.Add(Coordinate.GetInstance(newX, source.Y));
            }

            // Check if there's an opponent's piece to capture diagonally forward
            foreach (int offset in new int[] { -1, 1 })
            {
                int captureXForward = source.X + forwardDirection;
                int captureYForward = source.Y + offset;

                if (captureXForward >= 0 && captureXForward < 8 && captureYForward >= 0 && captureYForward < 8)
                {
                    Coordinate capturePosition = Coordinate.GetInstance(captureXForward, captureYForward);
                    if (layout.ContainsKey(capturePosition) && layout[capturePosition].Color != Color)
                    {
                        availableMoves.Add(capturePosition);
                    }
                }
            }

            // Check for capturing forward directly in front of the pawn
            int captureX = source.X + forwardDirection;
            int captureY = source.Y;
            if (captureX >= 0 && captureX < 8 && captureY >= 0 && captureY < 8)
            {
                Coordinate capturePosition = Coordinate.GetInstance(captureX, captureY);
                if (layout.ContainsKey(capturePosition) && layout[capturePosition].Color != Color)
                {
                    availableMoves.Add(capturePosition);
                }
            }

            // Check for the initial double move
            if ((Color == PieceColors.White && source.X == 1) || (Color == PieceColors.Black && source.X == 6))
            {
                int doubleMoveX = source.X + forwardDirection * 2;
                if (doubleMoveX >= 0 && doubleMoveX < 8 && !layout.ContainsKey(Coordinate.GetInstance(doubleMoveX, source.Y)))
                {
                    availableMoves.Add(Coordinate.GetInstance(doubleMoveX, source.Y));
                }
            }

            return availableMoves;
        }


    }
}

