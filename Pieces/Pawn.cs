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

        public override List<Coordinate> GetAvailableMoves(Coordinate source)
        {
            List<Coordinate> availableMoves = new List<Coordinate>();

            // Pawn moves forward one square (opposite direction)
            int forwardDirection = (Color == PieceColors.White) ? 1 : -1;
            int newX = source.X + forwardDirection;

            // Check if the new position is within the bounds of the board and if the square in front is empty
            if (newX >= 0 && newX < 8)
            {
                availableMoves.Add(Coordinate.GetInstance(newX, source.Y));
            }

            // Add initial double move for pawn
            if ((Color == PieceColors.White && source.X == 1) || (Color == PieceColors.Black && source.X == 6))
            {
                int doubleMoveX = source.X + forwardDirection * 2;
                // Check if the squares in front of the pawn are empty for both single and double moves
                if (doubleMoveX >= 0 && doubleMoveX < 8)
                {
                    availableMoves.Add(Coordinate.GetInstance(doubleMoveX, source.Y));
                }
            }

            return availableMoves;
        }
    }
}
