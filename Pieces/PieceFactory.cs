using System;

namespace Chess
{
    public class PieceFactory
    {
        public static APiece CreatePiece(PieceType type, PieceColors color)
        {
            switch (type)
            {
                case PieceType.Bishop:
                    return new Bishop(color);
                case PieceType.King:
                    return new King(color);
                case PieceType.Knight:
                    return new Knight(color);
                case PieceType.Pawn:
                    return new Pawn(color);
                case PieceType.Queen:
                    return new Queen(color);
                case PieceType.Rook:
                    return new Rook(color);
                default:
                    throw new ArgumentException("Invalid piece type.");
            }
        }
    }
}
//dict de coord si piese