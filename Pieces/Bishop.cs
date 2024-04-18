using System;
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
            int[] dx = { -1, 1, -1, 1 };
            int[] dy = { -1, -1, 1, 1 };

            for (int i = 0; i < 4; i++)
            {
                int newX = source.X + dx[i];
                int newY = source.Y + dy[i];

                while (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                {
                    if (!layout.ContainsKey(Coordinate.GetInstance(newX, newY)))
                    {
                        availableMoves.Add(Coordinate.GetInstance(newX, newY));
                    }
                    else if (layout[Coordinate.GetInstance(newX, newY)].Color != Color)
                    {
                        availableMoves.Add(Coordinate.GetInstance(newX, newY));
                        break;
                    }
                    else
                    {
                        break; // Path is blocked by a friendly piece
                    }

                    newX += dx[i];
                    newY += dy[i];
                }
            }

            return availableMoves;
        }
    }
}
