using Game.Framework;

namespace Game.Chess
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
            this.Add(Coordinate.GetInstance(7, 0), PieceFactory.CreatePiece(ChessPieceType.Rook, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 1), PieceFactory.CreatePiece(ChessPieceType.Knight, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 2), PieceFactory.CreatePiece(ChessPieceType.Bishop, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 3), PieceFactory.CreatePiece(ChessPieceType.Queen, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 4), PieceFactory.CreatePiece(ChessPieceType.King, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 5), PieceFactory.CreatePiece(ChessPieceType.Bishop, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 6), PieceFactory.CreatePiece(ChessPieceType.Knight, PieceColors.Black));
            this.Add(Coordinate.GetInstance(7, 7), PieceFactory.CreatePiece(ChessPieceType.Rook, PieceColors.Black));

            for (int i = 0; i < 8; i++)
            {
                this.Add(Coordinate.GetInstance(6, i), PieceFactory.CreatePiece(ChessPieceType.Pawn, PieceColors.Black));
                this.Add(Coordinate.GetInstance(1, i), PieceFactory.CreatePiece(ChessPieceType.Pawn, PieceColors.White));
            }

            this.Add(Coordinate.GetInstance(0, 0), PieceFactory.CreatePiece(ChessPieceType.Rook, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 1), PieceFactory.CreatePiece(ChessPieceType.Knight, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 2), PieceFactory.CreatePiece(ChessPieceType.Bishop, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 3), PieceFactory.CreatePiece(ChessPieceType.Queen, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 4), PieceFactory.CreatePiece(ChessPieceType.King, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 5), PieceFactory.CreatePiece(ChessPieceType.Bishop, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 6), PieceFactory.CreatePiece(ChessPieceType.Knight, PieceColors.White));
            this.Add(Coordinate.GetInstance(0, 7), PieceFactory.CreatePiece(ChessPieceType.Rook, PieceColors.White));
        }
    }
}
