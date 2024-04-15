using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
    public class Bishop : APiece
    {
        public Bishop(PieceColors color) : base(color, PieceType.Bishop)
        {
        }

        public override List<Coordinate> GetAvailableMoves(Coordinate source, ALayout layout)
        {
            List<Coordinate> availableMoves = new List<Coordinate>();

            // Bishop moves diagonally
            for (int dx = -1; dx <= 1; dx += 2)
            {
                for (int dy = -1; dy <= 1; dy += 2)
                {
                    for (int i = 1; i < 8; i++)
                    {
                        int newX = source.X + dx * i;
                        int newY = source.Y + dy * i;

                        // Check if the new position is within the bounds of the board
                        if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                        {
                            availableMoves.Add(Coordinate.GetInstance(newX, newY));
                        }
                    }
                }
            }

            return availableMoves;
        }
    }
}
