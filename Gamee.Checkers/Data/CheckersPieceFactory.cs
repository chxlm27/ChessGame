using Gamee.Framework;
using System;

namespace Gamee.Checkers
{
    public class CheckersPieceFactory
    {
        public static APiece CreatePiece(CheckersPieceType type, PieceColors color)
        {
            switch (type)
            {
                case CheckersPieceType.Man:
                    return new Man(color);
                case CheckersPieceType.King:
                    return new King(color);
                default:
                    throw new ArgumentException("Invalid checkers piece type.");
            }
        }
    }
}
