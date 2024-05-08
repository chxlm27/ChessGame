using Gamee.Framework;

namespace Gamee.Chess
{
    public class ChessLayout : ALayout
    {
        public ChessLayout() : base()
        {
        }
        public override ALayout Clone()
        {
            ChessLayout clone = new ChessLayout();
            foreach (var kvp in this)
            {
                clone.Add(kvp.Key, kvp.Value);
            }
            return clone;
        }
        public override void Initialize()
        {
            this.Add(Coordinate.GetInstance(7, 0), ChessPieceFactory.CreatePiece(ChessPieceType.Rook, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 1), ChessPieceFactory.CreatePiece(ChessPieceType.Knight, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 2), ChessPieceFactory.CreatePiece(ChessPieceType.Bishop, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 3), ChessPieceFactory.CreatePiece(ChessPieceType.Queen, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 4), ChessPieceFactory.CreatePiece(ChessPieceType.King, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 5), ChessPieceFactory.CreatePiece(ChessPieceType.Bishop, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 6), ChessPieceFactory.CreatePiece(ChessPieceType.Knight, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 7), ChessPieceFactory.CreatePiece(ChessPieceType.Rook, PieceColors.Black));

            for (int i = 0; i < 8; i++)
            {
                this.Add(Coordinate.GetInstance(6, i), ChessPieceFactory.CreatePiece(ChessPieceType.Pawn, PieceColors.Black));
                this.Add(Coordinate.GetInstance(1, i), ChessPieceFactory.CreatePiece(ChessPieceType.Pawn, PieceColors.White));
            }

            this.Add(Coordinate.GetInstance(0, 0), ChessPieceFactory.CreatePiece(ChessPieceType.Rook, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 1), ChessPieceFactory.CreatePiece(ChessPieceType.Knight, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 2), ChessPieceFactory.CreatePiece(ChessPieceType.Bishop, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 3), ChessPieceFactory.CreatePiece(ChessPieceType.Queen, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 4), ChessPieceFactory.CreatePiece(ChessPieceType.King, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 5), ChessPieceFactory.CreatePiece(ChessPieceType.Bishop, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 6), ChessPieceFactory.CreatePiece(ChessPieceType.Knight, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 7), ChessPieceFactory.CreatePiece(ChessPieceType.Rook, PieceColors.White));
        }
    }
}
