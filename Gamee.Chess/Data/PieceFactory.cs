using Gamee.Framework;

namespace Gamee.Chess
{
    public class PieceFactory
    {
        public static APiece CreatePiece(ChessPieceType type, PieceColors color)
        {
            switch (type)
            {
                case ChessPieceType.Bishop:
                    return new Bishop(color);
                case ChessPieceType.King:
                    return new King(color);
                case ChessPieceType.Knight:
                    return new Knight(color);
                case ChessPieceType.Pawn:
                    return new Pawn(color);
                case ChessPieceType.Queen:
                    return new Queen(color);
                case ChessPieceType.Rook:
                    return new Rook(color);
                default:
                    throw new ArgumentException("Invalid piece type.");
            }
        }
    }
}
//dict de coord si piese