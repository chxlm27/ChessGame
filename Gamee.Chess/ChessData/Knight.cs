﻿using Gamee.Framework;
using System.Collections.Generic;

namespace Gamee.Chess
{
    public class Knight : APiece
    {
        public Knight(PieceColors color) : base(color, new ChessPieceTypeAdapter(ChessPieceType.Knight))
        {
        }


        public override List<Coordinate> GetAvailableMoves(Coordinate source, ALayout layout)
        {
            List<Coordinate> availableMoves = new List<Coordinate>();

            // Knight moves in an "L" shape pattern
            int[] dx = { -2, -1, 1, 2, 2, 1, -1, -2 };
            int[] dy = { 1, 2, 2, 1, -1, -2, -2, -1 };

            for (int i = 0; i < dx.Length; i++)
            {
                int newX = source.X + dx[i];
                int newY = source.Y + dy[i];

                // Check if the new position is within the bounds of the board
                if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                {
                    // Check if the position is empty or occupied by an opponent's piece
                    if (!layout.ContainsKey(Coordinate.GetInstance(newX, newY)) ||
                        layout[Coordinate.GetInstance(newX, newY)].Color != Color)
                    {
                        availableMoves.Add(Coordinate.GetInstance(newX, newY));
                    }
                }
            }

            return availableMoves;
        }

    }
}
